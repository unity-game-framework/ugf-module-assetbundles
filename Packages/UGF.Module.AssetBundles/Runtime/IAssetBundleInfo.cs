using System.Collections.Generic;
using UGF.Module.Assets.Runtime;

namespace UGF.Module.AssetBundles.Runtime
{
    public interface IAssetBundleInfo : IAssetInfo
    {
        List<string> Dependencies { get; }
    }
}
