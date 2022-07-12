using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.EditorTools.Runtime.Assets;
using UGF.Module.Assets.Runtime;
using UnityEngine;

namespace UGF.Module.AssetBundles.Runtime
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Assets/Asset Bundle Module", order = 2000)]
    public class AssetBundleModuleAsset : ApplicationModuleAsset<AssetBundleModule, AssetBundleModuleDescription>
    {
        [SerializeField] private List<AssetIdReference<AssetBundleStorageAsset>> m_storages = new List<AssetIdReference<AssetBundleStorageAsset>>();
        [SerializeField] private List<AssetIdReference<AssetBundleAsset>> m_assetBundles = new List<AssetIdReference<AssetBundleAsset>>();

        public List<AssetIdReference<AssetBundleStorageAsset>> Storages { get { return m_storages; } }
        public List<AssetIdReference<AssetBundleAsset>> AssetBundles { get { return m_assetBundles; } }

        protected override IApplicationModuleDescription OnBuildDescription()
        {
            var description = new AssetBundleModuleDescription
            {
                RegisterType = typeof(AssetBundleModule)
            };

            for (int i = 0; i < m_storages.Count; i++)
            {
                AssetIdReference<AssetBundleStorageAsset> reference = m_storages[i];

                description.Storages.Add(reference.Guid, reference.Asset);
            }

            for (int i = 0; i < m_assetBundles.Count; i++)
            {
                AssetIdReference<AssetBundleAsset> reference = m_assetBundles[i];

                description.AssetBundles.Add(reference.Guid, reference.Asset);
            }

            return description;
        }

        protected override AssetBundleModule OnBuild(AssetBundleModuleDescription description, IApplication application)
        {
            var assetModule = application.GetModule<IAssetModule>();

            return new AssetBundleModule(description, application, assetModule);
        }
    }
}
