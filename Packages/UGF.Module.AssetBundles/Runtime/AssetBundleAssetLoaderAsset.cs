using UGF.Module.Assets.Runtime;
using UnityEngine;

namespace UGF.Module.AssetBundles.Runtime
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Assets/Asset Bundle Asset Loader", order = 2000)]
    public class AssetBundleAssetLoaderAsset : AssetLoaderAsset
    {
        [SerializeField] private AssetBundleAssetLoadParameters m_defaultLoadParameters = new AssetBundleAssetLoadParameters(new AssetBundleLoadParameters());
        [SerializeField] private AssetBundleAssetUnloadParameters m_defaultUnloadParameters = new AssetBundleAssetUnloadParameters(new AssetBundleUnloadParameters());

        public AssetBundleAssetLoadParameters DefaultLoadParameters { get { return m_defaultLoadParameters; } }
        public AssetBundleAssetUnloadParameters DefaultUnloadParameters { get { return m_defaultUnloadParameters; } }

        protected override IAssetLoader OnBuild()
        {
            return new AssetBundleAssetLoader(m_defaultLoadParameters, m_defaultUnloadParameters);
        }
    }
}
