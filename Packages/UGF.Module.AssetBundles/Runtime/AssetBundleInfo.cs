using System.Collections.Generic;
using UGF.Module.Assets.Runtime;

namespace UGF.Module.AssetBundles.Runtime
{
    public class AssetBundleInfo : AssetInfo, IAssetBundleInfo
    {
        public List<string> Dependencies { get; } = new List<string>();

        public AssetBundleInfo(string loaderId, string address) : base(loaderId, address)
        {
        }
    }
}
