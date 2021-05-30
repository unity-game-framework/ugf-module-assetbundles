using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.EditorTools.Runtime.IMGUI.AssetReferences;
using UGF.Module.Assets.Runtime;
using UnityEngine;

namespace UGF.Module.AssetBundles.Runtime
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Assets/Asset Bundle Module", order = 2000)]
    public class AssetBundleModuleAsset : ApplicationModuleAsset<AssetBundleModule, AssetBundleModuleDescription>
    {
        [SerializeField] private List<AssetReference<AssetBundleAsset>> m_assetBundles = new List<AssetReference<AssetBundleAsset>>();

        public List<AssetReference<AssetBundleAsset>> AssetBundles { get { return m_assetBundles; } }

        protected override IApplicationModuleDescription OnBuildDescription()
        {
            var description = new AssetBundleModuleDescription
            {
                RegisterType = typeof(AssetBundleModule)
            };

            for (int i = 0; i < m_assetBundles.Count; i++)
            {
                AssetReference<AssetBundleAsset> reference = m_assetBundles[i];

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
