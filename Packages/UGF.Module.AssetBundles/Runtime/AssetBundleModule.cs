using System;
using UGF.Application.Runtime;
using UGF.Builder.Runtime;
using UGF.EditorTools.Runtime.Ids;
using UGF.Logs.Runtime;
using UGF.Module.Assets.Runtime;
using UGF.RuntimeTools.Runtime.Providers;

namespace UGF.Module.AssetBundles.Runtime
{
    public class AssetBundleModule : ApplicationModule<AssetBundleModuleDescription>
    {
        public IProvider<GlobalId, IAssetBundleStorage> Storages { get; }
        public IAssetModule AssetModule { get; }

        public AssetBundleModule(AssetBundleModuleDescription description, IApplication application, IAssetModule assetModule) : this(description, application, new Provider<GlobalId, IAssetBundleStorage>(), assetModule)
        {
        }

        public AssetBundleModule(AssetBundleModuleDescription description, IApplication application, IProvider<GlobalId, IAssetBundleStorage> storages, IAssetModule assetModule) : base(description, application)
        {
            Storages = storages ?? throw new ArgumentNullException(nameof(storages));
            AssetModule = assetModule ?? throw new ArgumentNullException(nameof(assetModule));
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            Log.Debug("Asset Bundle Module initialize", new
            {
                storages = Description.Storages.Count,
                bundles = Description.AssetBundles.Count
            });

            foreach ((GlobalId key, IBuilder<IAssetBundleStorage> value) in Description.Storages)
            {
                IAssetBundleStorage storage = value.Build();

                Storages.Add(key, storage);
            }

            foreach ((GlobalId key, IBuilder<IAssetBundleInfo> value) in Description.AssetBundles)
            {
                IAssetBundleInfo info = value.Build();

                AssetModule.Assets.Add(key, info);
            }
        }

        protected override void OnUninitialize()
        {
            base.OnUninitialize();

            Log.Debug("Asset Bundle Module uninitialize", new
            {
                storages = Storages.Entries.Count,
                bundles = Description.AssetBundles.Count,
                Description.UnloadTrackedAssetBundlesOnUninitialize
            });

            if (Description.UnloadTrackedAssetBundlesOnUninitialize)
            {
                foreach ((GlobalId key, _) in Description.AssetBundles)
                {
                    if (AssetModule.Tracker.TryGet(key, out AssetTrack track))
                    {
                        AssetModule.Unload(key, track.Asset);
                    }
                }

                foreach ((GlobalId key, _) in Description.AssetBundles)
                {
                    AssetModule.Assets.Remove(key);
                }
            }

            Storages.Clear();
        }
    }
}
