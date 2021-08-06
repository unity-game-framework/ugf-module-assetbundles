using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UGF.AssetBundles.Editor;
using UGF.Module.AssetBundles.Runtime;
using UnityEditor;
using UnityEngine;
using Directory = UnityEngine.Windows.Directory;

namespace UGF.Module.AssetBundles.Editor
{
    public static class AssetBundleBuildEditorUtility
    {
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
                asset.Dependencies.AddRange(dependencies);

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
                string assetBundle = asset.AssetBundle;

                if (assetBundles.Contains(assetBundle))
                {
                    if (!groups.TryGetValue(assetBundle, out IList<string> list))
                    {
                        list = new List<string>();

                        groups.Add(assetBundle, list);
                    }

                    foreach (string assetGuid in asset.Assets)
                    {
                        list.Add(assetGuid);
                    }
                }
            }
        }
    }
}
