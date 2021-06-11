using UGF.Module.Assets.Runtime;
using UnityEngine;

namespace UGF.Module.AssetBundles.Runtime
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Assets/Asset Bundle File Loader", order = 2000)]
    public class AssetBundleFileLoaderAsset : AssetLoaderAsset
    {
        [SerializeField] private AssetBundleLoadParameters m_defaultLoadParameters = new AssetBundleLoadParameters();
        [SerializeField] private AssetBundleUnloadParameters m_defaultUnloadParameters = new AssetBundleUnloadParameters();

        public AssetBundleLoadParameters DefaultLoadParameters { get { return m_defaultLoadParameters; } }
        public AssetBundleUnloadParameters DefaultUnloadParameters { get { return m_defaultUnloadParameters; } }

        protected override IAssetLoader OnBuild()
        {
            return new AssetBundleFileLoader(m_defaultLoadParameters, m_defaultUnloadParameters);
        }
    }
}
