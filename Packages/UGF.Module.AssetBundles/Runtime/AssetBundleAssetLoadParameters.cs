using System;
using UGF.Module.Assets.Runtime;

namespace UGF.Module.AssetBundles.Runtime
{
    public class AssetBundleAssetLoadParameters : IAssetLoadParameters
    {
        public AssetBundleLoadParameters AssetBundleLoadParameters { get; }

        public static AssetBundleAssetLoadParameters Default { get; } = new AssetBundleAssetLoadParameters(AssetBundleLoadParameters.Default);

        public AssetBundleAssetLoadParameters(AssetBundleLoadParameters assetBundleLoadParameters)
        {
            AssetBundleLoadParameters = assetBundleLoadParameters ?? throw new ArgumentNullException(nameof(assetBundleLoadParameters));
        }
    }
}
