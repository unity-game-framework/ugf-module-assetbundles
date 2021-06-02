using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UGF.Module.AssetBundles.Editor
{
    public static class AssetBundleEditorUtility
    {
        public static AssetBundleManifest Build(IReadOnlyDictionary<string, IList<string>> groups, string outputPath, BuildAssetBundleOptions options = BuildAssetBundleOptions.None)
        {
            return Build(groups, outputPath, EditorUserBuildSettings.activeBuildTarget, options);
        }

        public static AssetBundleManifest Build(IReadOnlyDictionary<string, IList<string>> groups, string outputPath, BuildTarget target, BuildAssetBundleOptions options = BuildAssetBundleOptions.None)
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
