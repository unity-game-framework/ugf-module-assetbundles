using UGF.Module.Assets.Runtime;

namespace UGF.Module.AssetBundles.Runtime
{
    public class AssetBundleUnloadParameters : IAssetUnloadParameters
    {
        public bool UnloadAllLoadedObjects { get; }

        public static AssetBundleUnloadParameters Default { get; } = new AssetBundleUnloadParameters();

        public AssetBundleUnloadParameters(bool unloadAllLoadedObjects = true)
        {
            UnloadAllLoadedObjects = unloadAllLoadedObjects;
        }
    }
}
