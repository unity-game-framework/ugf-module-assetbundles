using UGF.Module.Assets.Runtime;
using UnityEngine;

namespace UGF.Module.AssetBundles.Runtime
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Assets/Asset Bundle File Loader", order = 2000)]
    public class AssetBundleFileLoaderAsset : AssetLoaderAsset
    {
        protected override IAssetLoader OnBuild()
        {
            return new AssetBundleFileLoader();
        }
    }
}
