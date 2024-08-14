using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.Builder.Runtime;
using UGF.EditorTools.Runtime.Assets;
using UGF.EditorTools.Runtime.Ids;
using UGF.Module.Assets.Runtime;
using UnityEngine;

namespace UGF.Module.AssetBundles.Runtime
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Assets/Asset Bundle Module", order = 2000)]
    public class AssetBundleModuleAsset : ApplicationModuleAsset<AssetBundleModule, AssetBundleModuleDescription>
    {
        [SerializeField] private bool m_unloadTrackedAssetBundlesOnUninitialize = true;
        [SerializeField] private List<AssetIdReference<AssetBundleStorageAsset>> m_storages = new List<AssetIdReference<AssetBundleStorageAsset>>();
        [SerializeField] private List<AssetIdReference<AssetBundleAsset>> m_assetBundles = new List<AssetIdReference<AssetBundleAsset>>();
        [SerializeField] private List<AssetBundleCollectionAsset> m_collections = new List<AssetBundleCollectionAsset>();

        public bool UnloadTrackedAssetBundlesOnUninitialize { get { return m_unloadTrackedAssetBundlesOnUninitialize; } set { m_unloadTrackedAssetBundlesOnUninitialize = value; } }
        public List<AssetIdReference<AssetBundleStorageAsset>> Storages { get { return m_storages; } }
        public List<AssetIdReference<AssetBundleAsset>> AssetBundles { get { return m_assetBundles; } }
        public List<AssetBundleCollectionAsset> Collections { get { return m_collections; } }

        protected override AssetBundleModuleDescription OnBuildDescription()
        {
            var storages = new Dictionary<GlobalId, IBuilder<IAssetBundleStorage>>();
            var assetBundles = new Dictionary<GlobalId, IBuilder<IAssetBundleInfo>>();

            for (int i = 0; i < m_storages.Count; i++)
            {
                AssetIdReference<AssetBundleStorageAsset> reference = m_storages[i];

                storages.Add(reference.Guid, reference.Asset);
            }

            for (int i = 0; i < m_assetBundles.Count; i++)
            {
                AssetIdReference<AssetBundleAsset> reference = m_assetBundles[i];

                assetBundles.Add(reference.Guid, reference.Asset);
            }

            for (int i = 0; i < m_collections.Count; i++)
            {
                AssetBundleCollectionAsset collection = m_collections[i];

                collection.GetAssetBundles(assetBundles);
            }

            return new AssetBundleModuleDescription(storages,
                assetBundles,
                m_unloadTrackedAssetBundlesOnUninitialize
            );
        }

        protected override AssetBundleModule OnBuild(AssetBundleModuleDescription description, IApplication application)
        {
            var assetModule = application.GetModule<IAssetModule>();

            return new AssetBundleModule(description, application, assetModule);
        }
    }
}
