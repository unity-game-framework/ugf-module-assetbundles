using UGF.EditorTools.Runtime.IMGUI.Attributes;
using UnityEngine;

namespace UGF.Module.AssetBundles.Runtime
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Assets/Asset Bundle File", order = 2000)]
    public class AssetBundleFileAsset : AssetBundleAsset
    {
        [AssetGuid(typeof(AssetBundleStorageAsset))]
        [SerializeField] private string m_storage;
        [SerializeField] private uint m_crc;
        [SerializeField] private ulong m_offset;

        public string Storage { get { return m_storage; } set { m_storage = value; } }
        public uint Crc { get { return m_crc; } set { m_crc = value; } }
        public ulong Offset { get { return m_offset; } set { m_offset = value; } }

        protected override IAssetBundleInfo OnBuild()
        {
            var info = new AssetBundleFileInfo(Loader, m_storage)
            {
                Crc = m_crc,
                Offset = m_offset
            };

            for (int i = 0; i < Dependencies.Count; i++)
            {
                string dependency = Dependencies[i];

                info.Dependencies.Add(dependency);
            }

            return info;
        }
    }
}
