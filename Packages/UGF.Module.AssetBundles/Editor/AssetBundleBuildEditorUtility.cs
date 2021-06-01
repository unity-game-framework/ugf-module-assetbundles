using System;
using System.Collections.Generic;
using UGF.Module.AssetBundles.Runtime;

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

        public static void GetGroupsAll(Dictionary<string, AssetBundleAsset> assetBundles)
        {
        }
    }
}
