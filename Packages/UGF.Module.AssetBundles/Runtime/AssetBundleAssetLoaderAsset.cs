using UGF.Module.Assets.Runtime;
using UnityEngine;

namespace UGF.Module.AssetBundles.Runtime
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Assets/Asset Bundle Asset Loader", order = 2000)]
    public class AssetBundleAssetLoaderAsset : AssetLoaderAsset
    {
        protected override IAssetLoader OnBuild()
        {
            return new AssetBundleAssetLoader();
        }
    }
}
