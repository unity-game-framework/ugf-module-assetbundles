using System.Collections.Generic;
using UGF.Application.Runtime;

namespace UGF.Module.AssetBundles.Runtime
{
    public class AssetBundleModuleDescription : ApplicationModuleDescription
    {
        public Dictionary<string, IAssetBundleStorageBuilder> Storages { get; } = new Dictionary<string, IAssetBundleStorageBuilder>();
        public Dictionary<string, IAssetBundleInfoBuilder> AssetBundles { get; } = new Dictionary<string, IAssetBundleInfoBuilder>();
    }
}
