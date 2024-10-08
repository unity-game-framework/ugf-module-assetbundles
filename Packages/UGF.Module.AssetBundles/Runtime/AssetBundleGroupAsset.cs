﻿using System.Collections.Generic;
using UGF.EditorTools.Runtime.Assets;
using UGF.EditorTools.Runtime.Ids;
using UGF.Module.Assets.Runtime;
using UnityEngine;

namespace UGF.Module.AssetBundles.Runtime
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Assets/Asset Bundle Group", order = 2000)]
    public class AssetBundleGroupAsset : AssetGroupAsset
    {
        [AssetId(typeof(AssetLoaderAsset))]
        [SerializeField] private Hash128 m_loader;
        [AssetId(typeof(AssetBundleAsset))]
        [SerializeField] private Hash128 m_assetBundle;
        [AssetId]
        [SerializeField] private List<Hash128> m_assets = new List<Hash128>();

        public GlobalId Loader { get { return m_loader; } set { m_loader = value; } }
        public GlobalId AssetBundle { get { return m_assetBundle; } set { m_assetBundle = value; } }
        public List<Hash128> Assets { get { return m_assets; } }

        protected override void OnGetAssets(IDictionary<GlobalId, IAssetInfo> assets)
        {
            for (int i = 0; i < m_assets.Count; i++)
            {
                GlobalId guid = m_assets[i];

                var info = new AssetBundleAssetInfo(m_loader, guid.ToString(), m_assetBundle);

                assets.Add(guid, info);
            }
        }
    }
}
