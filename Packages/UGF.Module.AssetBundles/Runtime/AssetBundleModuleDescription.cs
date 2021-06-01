using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.Builder.Runtime;

namespace UGF.Module.AssetBundles.Runtime
{
    public class AssetBundleModuleDescription : ApplicationModuleDescription
    {
        public Dictionary<string, IBuilder<IAssetBundleStorage>> Storages { get; } = new Dictionary<string, IBuilder<IAssetBundleStorage>>();
        public Dictionary<string, IBuilder<IAssetBundleInfo>> AssetBundles { get; } = new Dictionary<string, IBuilder<IAssetBundleInfo>>();
    }
}
