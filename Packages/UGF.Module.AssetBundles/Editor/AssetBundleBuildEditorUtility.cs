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

            var groups = new Dictionary<string, IList<string>>();

            GetGroupsAll(groups, buildAsset);

            foreach (KeyValuePair<string, IList<string>> pair in groups)
            {
                var build = new AssetBundleBuildInfo(pair.Key);

                for (int i = 0; i < pair.Value.Count; i++)
                {
                    string guid = pair.Value[i];
                    string path = AssetDatabase.GUIDToAssetPath(guid);

                    build.AddAsset(guid, path);
                }

                assetBundleBuilds.Add(build);
            }
        }

        public static void GetGroupsAll(IDictionary<string, IList<string>> groups, AssetBundleBuildAsset buildAsset)
        {
            if (groups == null) throw new ArgumentNullException(nameof(groups));
            if (buildAsset == null) throw new ArgumentNullException(nameof(buildAsset));

            var assetBundles = new List<string>();

            for (int i = 0; i < buildAsset.AssetBundles.Count; i++)
            {
                AssetBundleAsset asset = buildAsset.AssetBundles[i];
                string guid = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(asset));

                assetBundles.Add(guid);
            }

            GetGroupsAll(groups, assetBundles);
        }

        public static void GetGroupsAll(IDictionary<string, IList<string>> groups, IReadOnlyList<string> assetBundles)
        {
            if (groups == null) throw new ArgumentNullException(nameof(groups));
            if (assetBundles == null) throw new ArgumentNullException(nameof(assetBundles));

            string[] guids = AssetDatabase.FindAssets($"t:{nameof(AssetBundleGroupAsset)}");

            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                var asset = AssetDatabase.LoadAssetAtPath<AssetBundleGroupAsset>(path);
                string assetBundle = asset.AssetBundle.ToString();

                if (assetBundles.Contains(assetBundle))
                {
                    if (!groups.TryGetValue(assetBundle, out IList<string> list))
                    {
                        list = new List<string>();

                        groups.Add(assetBundle, list);
                    }

                    foreach (GlobalId assetGuid in asset.Assets)
                    {
                        list.Add(assetGuid.ToString());
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

            if (AssetBundleBuildUtility.DeleteManifestFiles(path))
            {
                string mainManifestPath = Path.Combine(path, Path.GetFileNameWithoutExtension(path));

                if (File.Exists(mainManifestPath))
                {
                    File.Delete(mainManifestPath);
                }

                return true;
            }

            return false;
        }
    }
}
