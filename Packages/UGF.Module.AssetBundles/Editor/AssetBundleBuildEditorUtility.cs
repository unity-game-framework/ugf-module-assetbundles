using System;
using System.Collections.Generic;
using System.Linq;
using UGF.EditorTools.Runtime.IMGUI.AssetReferences;
using UGF.Module.AssetBundles.Runtime;
using UnityEditor;

namespace UGF.Module.AssetBundles.Editor
{
    public static class AssetBundleBuildEditorUtility
    {
        public static void Build(AssetBundleBuildAsset buildAsset)
        {
            if (buildAsset == null) throw new ArgumentNullException(nameof(buildAsset));
        }

        public static void Refresh(AssetBundleBuildAsset buildAsset)
        {
            if (buildAsset == null) throw new ArgumentNullException(nameof(buildAsset));
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

            string[] guids = AssetDatabase.FindAssets($"t:{nameof(AssetBundleAsset)}");

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
