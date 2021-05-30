using System.Collections.Generic;
using UGF.EditorTools.Runtime.IMGUI.Attributes;
using UGF.Module.Assets.Runtime;
using UnityEngine;

namespace UGF.Module.AssetBundles.Runtime
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Assets/Asset Bundle File", order = 2000)]
    public class AssetBundleFileAsset : AssetBundleAsset
    {
        [AssetGuid(typeof(AssetLoaderAsset))]
        [SerializeField] private string m_loader;
        [SerializeField] private string m_file;
        [SerializeField] private uint m_crc;
        [SerializeField] private ulong m_offset;
        [AssetGuid(typeof(AssetBundleAsset))]
        [SerializeField] private List<string> m_dependencies = new List<string>();

        public string Loader { get { return m_loader; } set { m_loader = value; } }
        public string File { get { return m_file; } set { m_file = value; } }
        public uint Crc { get { return m_crc; } set { m_crc = value; } }
        public ulong Offset { get { return m_offset; } set { m_offset = value; } }
        public List<string> Dependencies { get { return m_dependencies; } }

        protected override void OnGetAssets(IDictionary<string, IAssetInfo> assets)
        {
            var info = new AssetBundleFileInfo(m_loader, m_file)
            {
                Crc = m_crc,
                Offset = m_offset
            };

            assets.Add("", info);
        }
    }
}
