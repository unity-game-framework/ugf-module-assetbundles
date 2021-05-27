using UGF.Module.Assets.Runtime;

namespace UGF.Module.AssetBundles.Runtime
{
    public class AssetBundleLoadParameters : IAssetLoadParameters
    {
        public static AssetBundleLoadParameters Default { get; } = new AssetBundleLoadParameters();
    }
}
