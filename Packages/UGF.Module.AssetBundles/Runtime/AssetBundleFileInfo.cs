namespace UGF.Module.AssetBundles.Runtime
{
    public class AssetBundleFileInfo : AssetBundleInfo
    {
        public uint Crc { get; set; }
        public ulong Offset { get; set; }

        public AssetBundleFileInfo(string loaderId, string address) : base(loaderId, address)
        {
        }
    }
}
