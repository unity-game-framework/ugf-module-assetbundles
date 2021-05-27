using System;
using System.Threading.Tasks;
using UGF.RuntimeTools.Runtime.Contexts;
using UnityEngine;

namespace UGF.Module.AssetBundles.Runtime
{
    public class AssetBundleLoaderFile : AssetBundleLoader<AssetBundleFileInfo, AssetBundleLoadParameters, AssetBundleUnloadParameters>
    {
        protected override AssetBundle OnLoadAssetBundle(AssetBundleFileInfo info, string id, Type type, AssetBundleLoadParameters parameters, IContext context)
        {
            AssetBundle assetBundle = AssetBundle.LoadFromFile(info.Address, info.Crc, info.Offset);

            return assetBundle;
        }

        protected override async Task<AssetBundle> OnLoadAssetBundleAsync(AssetBundleFileInfo info, string id, Type type, AssetBundleLoadParameters parameters, IContext context)
        {
            AssetBundleCreateRequest operation = AssetBundle.LoadFromFileAsync(info.Address, info.Crc, info.Offset);

            while (!operation.isDone)
            {
                await Task.Yield();
            }

            AssetBundle assetBundle = operation.assetBundle;

            return assetBundle;
        }
    }
}
