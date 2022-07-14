using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.Builder.Runtime;
using UGF.EditorTools.Runtime.Ids;

namespace UGF.Module.AssetBundles.Runtime
{
    public class AssetBundleModuleDescription : ApplicationModuleDescription
    {
        public Dictionary<GlobalId, IBuilder<IAssetBundleStorage>> Storages { get; } = new Dictionary<GlobalId, IBuilder<IAssetBundleStorage>>();
        public Dictionary<GlobalId, IBuilder<IAssetBundleInfo>> AssetBundles { get; } = new Dictionary<GlobalId, IBuilder<IAssetBundleInfo>>();
    }
}
