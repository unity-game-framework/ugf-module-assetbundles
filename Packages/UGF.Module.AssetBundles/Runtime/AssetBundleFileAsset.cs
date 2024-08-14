using UGF.EditorTools.Runtime.Assets;
using UGF.EditorTools.Runtime.Ids;
using UnityEngine;

namespace UGF.Module.AssetBundles.Runtime
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Assets/Asset Bundle File", order = 2000)]
    public class AssetBundleFileAsset : AssetBundleAsset
    {
        [AssetId(typeof(AssetBundleStorageAsset))]
        [SerializeField] private Hash128 m_storage;
        [SerializeField] private ulong m_offset;

        public GlobalId Storage { get { return m_storage; } set { m_storage = value; } }
        public ulong Offset { get { return m_offset; } set { m_offset = value; } }

        protected override IAssetBundleInfo OnBuild()
        {
            var info = new AssetBundleFileInfo(Loader, m_storage)
            {
                Crc = Crc,
                Offset = m_offset
            };

            for (int i = 0; i < Dependencies.Count; i++)
            {
                GlobalId dependency = Dependencies[i];

                info.Dependencies.Add(dependency);
            }

            return info;
        }
    }
}
