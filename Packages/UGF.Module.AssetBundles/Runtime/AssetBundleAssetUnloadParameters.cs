using System;
using UGF.Module.Assets.Runtime;
using UnityEngine;

namespace UGF.Module.AssetBundles.Runtime
{
    [Serializable]
    public class AssetBundleAssetUnloadParameters : IAssetUnloadParameters
    {
        [SerializeField] private AssetBundleUnloadParameters m_assetBundleUnloadParameters;

        public AssetBundleUnloadParameters AssetBundleUnloadParameters { get { return m_assetBundleUnloadParameters; } }

        public static AssetBundleAssetUnloadParameters Default { get; } = new AssetBundleAssetUnloadParameters(AssetBundleUnloadParameters.Default);

        public AssetBundleAssetUnloadParameters(AssetBundleUnloadParameters assetBundleUnloadParameters)
        {
            m_assetBundleUnloadParameters = assetBundleUnloadParameters ?? throw new ArgumentNullException(nameof(assetBundleUnloadParameters));
        }
    }
}
