using UGF.Module.Assets.Runtime;
using UnityEngine;

namespace UGF.Module.AssetBundles.Runtime
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Assets/Asset Bundle Loader File", order = 2000)]
    public class AssetBundleLoaderFileAsset : AssetLoaderAsset
    {
        protected override IAssetLoader OnBuild()
        {
            return new AssetBundleLoaderFile();
        }
    }
}
