using UGF.Builder.Runtime;
using UGF.Module.Assets.Runtime;

namespace UGF.Module.AssetBundles.Runtime
{
    public abstract class AssetBundleAsset : BuilderAsset<AssetBundleInfo>, IAssetBundleInfoBuilder
    {
        T IBuilder<IAssetInfo>.Build<T>()
        {
            return (T)(object)Build();
        }

        IAssetInfo IBuilder<IAssetInfo>.Build()
        {
            return Build();
        }
    }
}
