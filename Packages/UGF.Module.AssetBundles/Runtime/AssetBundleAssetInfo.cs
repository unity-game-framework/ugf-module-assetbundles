using System;
using UGF.EditorTools.Runtime.Ids;
using UGF.Module.Assets.Runtime;

namespace UGF.Module.AssetBundles.Runtime
{
    public class AssetBundleAssetInfo : AssetInfo
    {
        public GlobalId AssetBundleId { get; }

        public AssetBundleAssetInfo(GlobalId loaderId, string address, GlobalId assetBundleId) : base(loaderId, address)
        {
            if (!assetBundleId.IsValid()) throw new ArgumentException("Value should be valid.", nameof(assetBundleId));

            AssetBundleId = assetBundleId;
        }
    }
}
