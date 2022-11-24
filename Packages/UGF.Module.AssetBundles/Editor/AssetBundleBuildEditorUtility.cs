using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UGF.AssetBundles.Editor;
using UGF.EditorTools.Runtime.Ids;
using UGF.Module.AssetBundles.Runtime;
using UnityEditor;
using UnityEngine;

namespace UGF.Module.AssetBundles.Editor
{
    public static class AssetBundleBuildEditorUtility
    {
        public static void BuildAll(IReadOnlyList<AssetBundleBuildAsset> builds)
        {
            if (builds == null) throw new ArgumentNullException(nameof(builds));

            for (int i = 0; i < builds.Count; i++)
            {
                Build(builds[i]);
            }
        }

        public static void Build(AssetBundleBuildAsset buildAsset)
        {
            Build(buildAsset, EditorUserBuildSettings.activeBuildTarget);
        }

        public static void Build(AssetBundleBuildAsset buildAsset, BuildTarget target)
        {
            if (buildAsset == null) throw new ArgumentNullException(nameof(buildAsset));

            Directory.CreateDirectory(buildAsset.OutputPath);

            var builds = new List<AssetBundleBuildInfo>();

            GetAssetBundleBuilds(builds, buildAsset);

            AssetBundleManifest manifest = AssetBundleBuildUtility.Build(builds, buildAsset.OutputPath, target, buildAsset.Options);

            if (manifest != null)
            {
                if (buildAsset.UpdateCrc)
                {
                    UpdateCrc(buildAsset);
                }

                if (buildAsset.UpdateDependencies)
                {
                    UpdateDependencies(buildAsset, manifest);
                }

                if (buildAsset.ClearManifests)
                {
                    ClearManifests(buildAsset.OutputPath);
                }
            }
        }

        public static void UpdateCrc(AssetBundleBuildAsset buildAsset)
        {
            if (buildAsset == null) throw new ArgumentNullException(nameof(buildAsset));

            for (int i = 0; i < buildAsset.AssetBundles.Count; i++)
            {
                AssetBundleAsset asset = buildAsset.AssetBundles[i];

                string guid = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(asset));
                string path = Path.Combine(buildAsset.OutputPath, guid);

                if (BuildPipeline.GetCRCForAssetBundle(path, out uint crc))
                {
                    asset.Crc = crc;

                    EditorUtility.SetDirty(asset);
                }
            }
        }

        public static void UpdateDependencies(AssetBundleBuildAsset buildAsset, AssetBundleManifest manifest)
        {
            if (buildAsset == null) throw new ArgumentNullException(nameof(buildAsset));
            if (manifest == null) throw new ArgumentNullException(nameof(manifest));

            for (int i = 0; i < buildAsset.AssetBundles.Count; i++)
            {
                AssetBundleAsset asset = buildAsset.AssetBundles[i];

                string guid = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(asset));
                string[] dependencies = manifest.GetDirectDependencies(guid);

                asset.Dependencies.Clear();

                foreach (string dependency in dependencies)
                {
                    asset.Dependencies.Add(new GlobalId(dependency));
                }

                EditorUtility.SetDirty(asset);
            }
        }

        public static void GetAssetBundleBuilds(IList<AssetBundleBuildInfo> assetBundleBuilds, AssetBundleBuildAsset buildAsset)
        {
            if (assetBundleBuilds == null) throw new ArgumentNullException(nameof(assetBundleBuilds));
            if (buildAsset == null) throw new ArgumentNullException(nameof(buildAsset));

            var groups = new Dictionary<GlobalId, ISet<GlobalId>>();

            GetGroupsAll(groups, buildAsset);

            foreach ((GlobalId key, ISet<GlobalId> value) in groups)
            {
                var build = new AssetBundleBuildInfo(key.ToString());

                foreach (GlobalId id in value)
                {
                    string guid = id.ToString();
                    string path = AssetDatabase.GUIDToAssetPath(guid);

                    build.AddAsset(guid, path);
                }

                assetBundleBuilds.Add(build);
            }
        }

        public static void GetGroupsAll(IDictionary<GlobalId, ISet<GlobalId>> groups, AssetBundleBuildAsset buildAsset)
        {
            if (groups == null) throw new ArgumentNullException(nameof(groups));
            if (buildAsset == null) throw new ArgumentNullException(nameof(buildAsset));

            var assetBundles = new List<GlobalId>();

            for (int i = 0; i < buildAsset.AssetBundles.Count; i++)
            {
                AssetBundleAsset asset = buildAsset.AssetBundles[i];
                string guid = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(asset));

                assetBundles.Add(new GlobalId(guid));
            }

            GetGroupsAll(groups, assetBundles, buildAsset.IncludeDependencies);
        }

        /// <summary>
        /// Collects collection of asset ids per bundle from specified list of the asset bundle ids.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The <c>GlobalId</c> value represents <c>Guid</c> of an asset or asset bundle.
        /// </para>
        /// <para>
        /// Enabling <see cref="includeDependencies"/> parameter will collect dependencies for each asset in asset bundle
        /// and make them to be explicitly included with asset guid as address.
        /// If the next asset bundle would have any asset with dependencies already included in previous asset bundle,
        /// they will be ignored and asset bundle will have previous one as dependency.
        /// </para>
        /// </remarks>
        /// <param name="groups">The collect to fill.</param>
        /// <param name="assetBundles">The list of asset bundles to collect from.</param>
        /// <param name="includeDependencies">The value which determines whether to include dependencies of each asset inside of asset bundle.</param>
        public static void GetGroupsAll(IDictionary<GlobalId, ISet<GlobalId>> groups, IReadOnlyList<GlobalId> assetBundles, bool includeDependencies = false)
        {
            if (groups == null) throw new ArgumentNullException(nameof(groups));
            if (assetBundles == null) throw new ArgumentNullException(nameof(assetBundles));

            string[] guids = AssetDatabase.FindAssets($"t:{nameof(AssetBundleGroupAsset)}");
            var assets = new Dictionary<GlobalId, List<AssetBundleGroupAsset>>();

            foreach (string guid in guids)
            {
                var asset = AssetDatabase.LoadAssetAtPath<AssetBundleGroupAsset>(AssetDatabase.GUIDToAssetPath(guid));

                if (assetBundles.Contains(asset.AssetBundle))
                {
                    if (!assets.TryGetValue(asset.AssetBundle, out List<AssetBundleGroupAsset> list))
                    {
                        list = new List<AssetBundleGroupAsset>();

                        assets.Add(asset.AssetBundle, list);
                    }

                    list.Add(asset);
                }
            }

            for (int i = 0; i < assetBundles.Count; i++)
            {
                GlobalId id = assetBundles[i];

                if (assets.TryGetValue(id, out List<AssetBundleGroupAsset> list))
                {
                    foreach (AssetBundleGroupAsset asset in list)
                    {
                        GetGroupAssets(groups, asset, includeDependencies);
                    }
                }
            }
        }

        public static void GetGroupAssets(IDictionary<GlobalId, ISet<GlobalId>> groups, AssetBundleGroupAsset asset, bool includeDependencies = false)
        {
            if (groups == null) throw new ArgumentNullException(nameof(groups));
            if (asset == null) throw new ArgumentNullException(nameof(asset));

            GlobalId assetBundleId = asset.AssetBundle;

            if (!groups.TryGetValue(assetBundleId, out ISet<GlobalId> list))
            {
                list = new HashSet<GlobalId>();

                groups.Add(assetBundleId, list);
            }

            foreach (GlobalId assetGuid in asset.Assets)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(assetGuid.ToString());

                if (includeDependencies && Path.GetExtension(assetPath) != ".unity")
                {
                    string[] dependencies = AssetDatabase.GetDependencies(assetPath, true);

                    foreach (string dependencyPath in dependencies)
                    {
                        if (IsAllowedForAssetBundle(dependencyPath))
                        {
                            string dependencyGuid = AssetDatabase.AssetPathToGUID(dependencyPath);
                            var dependencyId = new GlobalId(dependencyGuid);

                            if (!IsAnyGroupContains(groups, dependencyId))
                            {
                                list.Add(dependencyId);
                            }
                        }
                    }
                }
                else
                {
                    list.Add(assetGuid);
                }
            }
        }

        public static void GetDependencies(ICollection<GlobalId> dependencies, IReadOnlyList<AssetBundleGroupAsset> groups)
        {
            if (dependencies == null) throw new ArgumentNullException(nameof(dependencies));
            if (groups == null) throw new ArgumentNullException(nameof(groups));

            var assets = new List<GlobalId>();

            for (int i = 0; i < groups.Count; i++)
            {
                AssetBundleGroupAsset group = groups[i];

                assets.AddRange(group.Assets);
            }

            GetDependencies(dependencies, assets);
        }

        public static void GetDependencies(ICollection<GlobalId> dependencies, IReadOnlyList<GlobalId> assets)
        {
            if (dependencies == null) throw new ArgumentNullException(nameof(dependencies));
            if (assets == null) throw new ArgumentNullException(nameof(assets));

            for (int i = 0; i < assets.Count; i++)
            {
                GlobalId assetId = assets[i];
                string assetPath = AssetDatabase.GUIDToAssetPath(assetId.ToString());
                string[] paths = AssetDatabase.GetDependencies(assetPath, true);

                foreach (string path in paths)
                {
                    if (IsAllowedForAssetBundle(path))
                    {
                        string guid = AssetDatabase.AssetPathToGUID(path);
                        var id = new GlobalId(guid);

                        if (!assets.Contains(id) && !dependencies.Contains(id))
                        {
                            dependencies.Add(id);
                        }
                    }
                }
            }
        }

        public static void ClearAll(IReadOnlyList<AssetBundleBuildAsset> builds)
        {
            if (builds == null) throw new ArgumentNullException(nameof(builds));

            for (int i = 0; i < builds.Count; i++)
            {
                Clear(builds[i]);
            }
        }

        public static void Clear(AssetBundleBuildAsset buildAsset)
        {
            if (buildAsset == null) throw new ArgumentNullException(nameof(buildAsset));

            string outputPath = buildAsset.OutputPath;
            string metaPath = $"{outputPath}.meta";

            if (Directory.Exists(outputPath))
            {
                Directory.Delete(outputPath, true);
            }

            if (File.Exists(metaPath))
            {
                File.Delete(metaPath);
            }
        }

        public static bool ClearManifests(string path)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentException("Value cannot be null or empty.", nameof(path));

            string projectPath = Environment.CurrentDirectory;
            string[] paths = Directory.GetFiles(path, "*.manifest", SearchOption.AllDirectories);

            for (int i = 0; i < paths.Length; i++)
            {
                string manifestPath = paths[i].Replace('\\', '/');

                if (Path.GetFullPath(manifestPath).StartsWith(projectPath, StringComparison.OrdinalIgnoreCase))
                {
                    AssetDatabase.DeleteAsset(manifestPath);
                }
                else
                {
                    File.Delete(manifestPath);
                }
            }

            string mainManifestPath = Path.Combine(path, Path.GetFileNameWithoutExtension(path));

            if (File.Exists(mainManifestPath))
            {
                if (Path.GetFullPath(mainManifestPath).StartsWith(projectPath))
                {
                    AssetDatabase.DeleteAsset(mainManifestPath);
                }
                else
                {
                    File.Delete(mainManifestPath);
                }
            }

            return paths.Length > 0;
        }

        private static bool IsAllowedForAssetBundle(string path)
        {
            Type type = AssetDatabase.GetMainAssetTypeAtPath(path);

            return type != typeof(MonoScript);
        }

        private static bool IsAnyGroupContains(IDictionary<GlobalId, ISet<GlobalId>> groups, GlobalId id)
        {
            foreach ((_, ISet<GlobalId> value) in groups)
            {
                if (value.Contains(id))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
