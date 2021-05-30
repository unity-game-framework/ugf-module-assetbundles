using System.Collections.Generic;
using UGF.Application.Runtime;

namespace UGF.Module.AssetBundles.Runtime
{
    public class AssetBundleModuleDescription : ApplicationModuleDescription
    {
        public Dictionary<string, IAssetBundleInfoBuilder> AssetBundles { get; } = new Dictionary<string, IAssetBundleInfoBuilder>();
    }
}
