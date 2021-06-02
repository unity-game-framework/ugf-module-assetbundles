using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UGF.EditorTools.Runtime.IMGUI.AssetReferences;
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
            if (buildAsset == null) throw new ArgumentNullException(nameof(buildAsset));

            Directory.CreateDirectory(buildAsset.OutputPath);

            var groups = new Dictionary<string, IList<string>>();

            GetGroupsAll(groups, buildAsset);

            AssetBundleManifest manifest = AssetBundleEditorUtility.Build(groups, buildAsset.OutputPath, buildAsset.Options);

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
                AssetReference<AssetBundleAsset> reference = buildAsset.AssetBundles[i];
                AssetBundleAsset asset = reference.Asset;

                string path = Path.Combine(buildAsset.OutputPath, reference.Guid);

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
                AssetReference<AssetBundleAsset> reference = buildAsset.AssetBundles[i];
                AssetBundleAsset asset = reference.Asset;

                string[] dependencies = manifest.GetDirectDependencies(reference.Guid);

                asset.Dependencies.Clear();
                asset.Dependencies.AddRange(dependencies);

                EditorUtility.SetDirty(asset);
            }
        }

        public static void GetGroupsAll(IDictionary<string, IList<string>> groups, AssetBundleBuildAsset buildAsset)
        {
            if (groups == null) throw new ArgumentNullException(nameof(groups));
            if (buildAsset == null) throw new ArgumentNullException(nameof(buildAsset));

            var assetBundles = new List<string>();

            for (int i = 0; i < buildAsset.AssetBundles.Count; i++)
            {
                AssetReference<AssetBundleAsset> assetBundle = buildAsset.AssetBundles[i];

                assetBundles.Add(assetBundle.Guid);
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
