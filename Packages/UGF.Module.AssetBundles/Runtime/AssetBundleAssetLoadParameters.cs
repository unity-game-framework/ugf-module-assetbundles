using System;
using UGF.Module.Assets.Runtime;
using UnityEngine;

namespace UGF.Module.AssetBundles.Runtime
{
    [Serializable]
    public class AssetBundleAssetLoadParameters : IAssetLoadParameters
    {
        [SerializeField] private AssetBundleLoadParameters m_assetBundleLoadParameters;

        public AssetBundleLoadParameters AssetBundleLoadParameters { get { return m_assetBundleLoadParameters; } }

        public static AssetBundleAssetLoadParameters Default { get; } = new AssetBundleAssetLoadParameters(AssetBundleLoadParameters.Default);

        public AssetBundleAssetLoadParameters(AssetBundleLoadParameters assetBundleLoadParameters)
        {
            m_assetBundleLoadParameters = assetBundleLoadParameters ?? throw new ArgumentNullException(nameof(assetBundleLoadParameters));
        }
    }
}
