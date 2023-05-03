using System;
using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.Builder.Runtime;
using UGF.EditorTools.Runtime.Ids;

namespace UGF.Module.AssetBundles.Runtime
{
    public class AssetBundleModuleDescription : ApplicationModuleDescription
    {
        public IReadOnlyDictionary<GlobalId, IBuilder<IAssetBundleStorage>> Storages { get; }
        public IReadOnlyDictionary<GlobalId, IBuilder<IAssetBundleInfo>> AssetBundles { get; }
        public bool UnloadTrackedAssetBundlesOnUninitialize { get; set; }

        public AssetBundleModuleDescription(
            Type registerType,
            IReadOnlyDictionary<GlobalId, IBuilder<IAssetBundleStorage>> storages,
            IReadOnlyDictionary<GlobalId, IBuilder<IAssetBundleInfo>> assetBundles,
            bool unloadTrackedAssetBundlesOnUninitialize) : base(registerType)
        {
            Storages = storages ?? throw new ArgumentNullException(nameof(storages));
            AssetBundles = assetBundles ?? throw new ArgumentNullException(nameof(assetBundles));
            UnloadTrackedAssetBundlesOnUninitialize = unloadTrackedAssetBundlesOnUninitialize;
        }
    }
}
