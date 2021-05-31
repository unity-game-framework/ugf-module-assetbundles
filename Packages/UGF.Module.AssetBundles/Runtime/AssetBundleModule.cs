using System;
using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.Module.Assets.Runtime;
using UGF.RuntimeTools.Runtime.Providers;

namespace UGF.Module.AssetBundles.Runtime
{
    public class AssetBundleModule : ApplicationModule<AssetBundleModuleDescription>
    {
        public IProvider<string, IAssetBundleStorage> Storages { get; }
        public IAssetModule AssetModule { get; }

        public AssetBundleModule(AssetBundleModuleDescription description, IApplication application, IAssetModule assetModule) : this(description, application, new Provider<string, IAssetBundleStorage>(), assetModule)
        {
        }

        public AssetBundleModule(AssetBundleModuleDescription description, IApplication application, IProvider<string, IAssetBundleStorage> storages, IAssetModule assetModule) : base(description, application)
        {
            Storages = storages ?? throw new ArgumentNullException(nameof(storages));
            AssetModule = assetModule ?? throw new ArgumentNullException(nameof(assetModule));
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            foreach (KeyValuePair<string, IAssetBundleStorageBuilder> pair in Description.Storages)
            {
                IAssetBundleStorage storage = pair.Value.Build();

                Storages.Add(pair.Key, storage);
            }

            foreach (KeyValuePair<string, IAssetBundleInfoBuilder> pair in Description.AssetBundles)
            {
                IAssetInfo info = pair.Value.Build();

                AssetModule.Assets.Add(pair.Key, info);
            }
        }

        protected override void OnUninitialize()
        {
            base.OnUninitialize();

            foreach (KeyValuePair<string, IAssetBundleInfoBuilder> pair in Description.AssetBundles)
            {
                AssetModule.Assets.Remove(pair.Key);
            }

            Storages.Clear();
        }
    }
}
