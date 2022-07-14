using System;
using UGF.EditorTools.Runtime.Ids;

namespace UGF.Module.AssetBundles.Runtime
{
    public class AssetBundleFileInfo : AssetBundleInfo
    {
        public GlobalId StorageId { get; }
        public uint Crc { get; set; }
        public ulong Offset { get; set; }

        public AssetBundleFileInfo(GlobalId loaderId, GlobalId storageId) : base(loaderId, storageId.ToString())
        {
            if (!storageId.IsValid()) throw new ArgumentException("Value should be valid.", nameof(storageId));

            StorageId = storageId;
        }
    }
}
