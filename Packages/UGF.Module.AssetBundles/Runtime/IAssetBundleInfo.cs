using System.Collections.Generic;
using UGF.EditorTools.Runtime.Ids;
using UGF.Module.Assets.Runtime;

namespace UGF.Module.AssetBundles.Runtime
{
    public interface IAssetBundleInfo : IAssetInfo
    {
        List<GlobalId> Dependencies { get; }
    }
}
