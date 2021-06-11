using System;
using UGF.Module.Assets.Runtime;

namespace UGF.Module.AssetBundles.Runtime
{
    [Serializable]
    public class AssetBundleLoadParameters : IAssetLoadParameters
    {
        public static AssetBundleLoadParameters Default { get; } = new AssetBundleLoadParameters();
    }
}
