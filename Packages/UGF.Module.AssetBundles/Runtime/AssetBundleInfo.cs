using System.Collections.Generic;
using UGF.EditorTools.Runtime.Ids;
using UGF.Module.Assets.Runtime;

namespace UGF.Module.AssetBundles.Runtime
{
    public class AssetBundleInfo : AssetInfo, IAssetBundleInfo
    {
        public List<GlobalId> Dependencies { get; } = new List<GlobalId>();

        public AssetBundleInfo(GlobalId loaderId, string address) : base(loaderId, address)
        {
        }
    }
}
