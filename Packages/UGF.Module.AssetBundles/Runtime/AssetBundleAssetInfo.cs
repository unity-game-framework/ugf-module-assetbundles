using System;
using UGF.Module.Assets.Runtime;

namespace UGF.Module.AssetBundles.Runtime
{
    public class AssetBundleAssetInfo : AssetInfo
    {
        public string AssetBundleId { get; }

        public AssetBundleAssetInfo(string loaderId, string address, string assetBundleId) : base(loaderId, address)
        {
            if (string.IsNullOrEmpty(assetBundleId)) throw new ArgumentException("Value cannot be null or empty.", nameof(assetBundleId));

            AssetBundleId = assetBundleId;
        }
    }
}
