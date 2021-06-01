using System;
using System.Collections.Generic;
using UGF.Module.AssetBundles.Runtime;
using UnityEditor;
using UnityEngine;

namespace UGF.Module.AssetBundles.Editor
{
    public static class AssetBundleEditorUtility
    {
        [MenuItem("Tests/TestBuildAll")]
        public static void TestBuildAll()
        {
            var groups = new Dictionary<string, IList<string>>();
            string[] guids = AssetDatabase.FindAssets($"t:{nameof(AssetBundleGroupAsset)}");

            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                var group = AssetDatabase.LoadAssetAtPath<AssetBundleGroupAsset>(path);

                if (!groups.TryGetValue(group.AssetBundle, out IList<string> assets))
                {
                    assets = new List<string>();

                    groups.Add(group.AssetBundle, assets);
                }

                foreach (string asset in group.Assets)
                {
                    assets.Add(asset);
                }
            }

            Build(groups, Application.streamingAssetsPath, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
        }

        public static AssetBundleManifest Build(IReadOnlyDictionary<string, IList<string>> groups, string outputPath, BuildAssetBundleOptions options, BuildTarget target)
        {
            if (groups == null) throw new ArgumentNullException(nameof(groups));
            if (string.IsNullOrEmpty(outputPath)) throw new ArgumentException("Value cannot be null or empty.", nameof(outputPath));

            var builds = new AssetBundleBuild[groups.Count];
            int index = 0;

            foreach (KeyValuePair<string, IList<string>> pair in groups)
            {
                var build = new AssetBundleBuild
                {
                    assetBundleName = pair.Key,
                    addressableNames = new string[pair.Value.Count],
                    assetNames = new string[pair.Value.Count]
                };

                for (int i = 0; i < pair.Value.Count; i++)
                {
                    string guid = pair.Value[i];
                    string path = AssetDatabase.GUIDToAssetPath(guid);

                    build.addressableNames[i] = guid;
                    build.assetNames[i] = path;
                }

                builds[index++] = build;
            }

            AssetBundleManifest manifest = BuildPipeline.BuildAssetBundles(outputPath, builds, options, target);

            return manifest;
        }
    }
}
