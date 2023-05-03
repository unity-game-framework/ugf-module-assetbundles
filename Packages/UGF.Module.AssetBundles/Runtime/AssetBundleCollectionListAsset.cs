using System.Collections.Generic;
using UGF.Builder.Runtime;
using UGF.EditorTools.Runtime.Assets;
using UGF.EditorTools.Runtime.Ids;
using UnityEngine;

namespace UGF.Module.AssetBundles.Runtime
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Assets/Asset Bundle Collection List", order = 2000)]
    public class AssetBundleCollectionListAsset : AssetBundleCollectionAsset
    {
        [SerializeField] private List<AssetIdReference<AssetBundleAsset>> m_assetBundles = new List<AssetIdReference<AssetBundleAsset>>();

        public List<AssetIdReference<AssetBundleAsset>> AssetBundles { get { return m_assetBundles; } }

        protected override void OnGetAssetBundles(IDictionary<GlobalId, IBuilder<IAssetBundleInfo>> assetBundles)
        {
            for (int i = 0; i < m_assetBundles.Count; i++)
            {
                AssetIdReference<AssetBundleAsset> reference = m_assetBundles[i];

                assetBundles.Add(reference.Guid, reference.Asset);
            }
        }
    }
}
