using System;
using UGF.Module.Assets.Runtime;

namespace UGF.Module.AssetBundles.Runtime
{
    public class AssetBundleAssetUnloadParameters : IAssetUnloadParameters
    {
        public AssetBundleUnloadParameters AssetBundleUnloadParameters { get; }

        public static AssetBundleAssetUnloadParameters Default { get; } = new AssetBundleAssetUnloadParameters(AssetBundleUnloadParameters.Default);

        public AssetBundleAssetUnloadParameters(AssetBundleUnloadParameters assetBundleUnloadParameters)
        {
            AssetBundleUnloadParameters = assetBundleUnloadParameters ?? throw new ArgumentNullException(nameof(assetBundleUnloadParameters));
        }
    }
}
