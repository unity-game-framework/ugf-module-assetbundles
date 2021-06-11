using System;
using UGF.Module.Assets.Runtime;
using UnityEngine;

namespace UGF.Module.AssetBundles.Runtime
{
    [Serializable]
    public class AssetBundleUnloadParameters : IAssetUnloadParameters
    {
        [SerializeField] private bool m_unloadAllLoadedObjects;

        public bool UnloadAllLoadedObjects { get { return m_unloadAllLoadedObjects; } }

        public static AssetBundleUnloadParameters Default { get; } = new AssetBundleUnloadParameters();

        public AssetBundleUnloadParameters(bool unloadAllLoadedObjects = true)
        {
            m_unloadAllLoadedObjects = unloadAllLoadedObjects;
        }
    }
}
