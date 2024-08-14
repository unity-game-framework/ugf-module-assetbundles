using System.Collections.Generic;
using UGF.Builder.Runtime;
using UGF.EditorTools.Runtime.Assets;
using UGF.EditorTools.Runtime.Ids;
using UGF.Module.Assets.Runtime;
using UnityEngine;

namespace UGF.Module.AssetBundles.Runtime
{
    public abstract class AssetBundleAsset : BuilderAsset<IAssetBundleInfo>
    {
        [AssetId(typeof(AssetLoaderAsset))]
        [SerializeField] private Hash128 m_loader;
        [SerializeField] private uint m_crc;
        [AssetId(typeof(AssetBundleAsset))]
        [SerializeField] private List<Hash128> m_dependencies = new List<Hash128>();

        public GlobalId Loader { get { return m_loader; } set { m_loader = value; } }
        public uint Crc { get { return m_crc; } set { m_crc = value; } }
        public List<Hash128> Dependencies { get { return m_dependencies; } }
    }
}
