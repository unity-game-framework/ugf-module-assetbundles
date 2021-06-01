using System.Collections.Generic;
using UGF.EditorTools.Runtime.IMGUI.Attributes;
using UGF.Module.Assets.Runtime;
using UnityEngine;

namespace UGF.Module.AssetBundles.Runtime
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Assets/Asset Bundle Group", order = 2000)]
    public class AssetBundleGroupAsset : AssetGroupAsset
    {
        [AssetGuid(typeof(AssetLoaderAsset))]
        [SerializeField] private string m_loader;
        [AssetGuid(typeof(AssetBundleAsset))]
        [SerializeField] private string m_assetBundle;
        [AssetGuid]
        [SerializeField] private List<string> m_assets = new List<string>();

        public string Loader { get { return m_loader; } set { m_loader = value; } }
        public string AssetBundle { get { return m_assetBundle; } set { m_assetBundle = value; } }
        public List<string> Assets { get { return m_assets; } }

        protected override void OnGetAssets(IDictionary<string, IAssetInfo> assets)
        {
            for (int i = 0; i < m_assets.Count; i++)
            {
                string guid = m_assets[i];
                var info = new AssetBundleAssetInfo(m_loader, guid, m_assetBundle);

                assets.Add(guid, info);
            }
        }
    }
}
