using System;
using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.Module.Assets.Runtime;

namespace UGF.Module.AssetBundles.Runtime
{
    public class AssetBundleModule : ApplicationModule<AssetBundleModuleDescription>
    {
        public IAssetModule AssetModule { get; }

        public AssetBundleModule(AssetBundleModuleDescription description, IApplication application, IAssetModule assetModule) : base(description, application)
        {
            AssetModule = assetModule ?? throw new ArgumentNullException(nameof(assetModule));
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

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
        }
    }
}
