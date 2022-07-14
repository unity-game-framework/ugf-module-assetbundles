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
        [SerializeField] private GlobalId m_loader;
        [SerializeField] private uint m_crc;
        [AssetId(typeof(AssetBundleAsset))]
        [SerializeField] private List<GlobalId> m_dependencies = new List<GlobalId>();

        public GlobalId Loader { get { return m_loader; } set { m_loader = value; } }
        public uint Crc { get { return m_crc; } set { m_crc = value; } }
        public List<GlobalId> Dependencies { get { return m_dependencies; } }
    }
}
