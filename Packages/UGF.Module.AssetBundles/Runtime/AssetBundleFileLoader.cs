using System;
using System.Threading.Tasks;
using UGF.Application.Runtime;
using UGF.RuntimeTools.Runtime.Contexts;
using UnityEngine;

namespace UGF.Module.AssetBundles.Runtime
{
    public class AssetBundleFileLoader : AssetBundleLoader<AssetBundleFileInfo, AssetBundleLoadParameters, AssetBundleUnloadParameters>
    {
        protected override AssetBundle OnLoadAssetBundle(AssetBundleFileInfo info, string id, Type type, AssetBundleLoadParameters parameters, IContext context)
        {
            var application = context.Get<IApplication>();
            var assetBundleModule = application.GetModule<AssetBundleModule>();
            IAssetBundleStorage storage = assetBundleModule.Storages.Get(info.Address);
            string address = storage.GetAddress(info, id, type, parameters, context);

            AssetBundle assetBundle = AssetBundle.LoadFromFile(address, info.Crc, info.Offset);

            return assetBundle;
        }

        protected override async Task<AssetBundle> OnLoadAssetBundleAsync(AssetBundleFileInfo info, string id, Type type, AssetBundleLoadParameters parameters, IContext context)
        {
            var application = context.Get<IApplication>();
            var assetBundleModule = application.GetModule<AssetBundleModule>();
            IAssetBundleStorage storage = assetBundleModule.Storages.Get(info.Address);
            string address = storage.GetAddress(info, id, type, parameters, context);

            AssetBundleCreateRequest operation = AssetBundle.LoadFromFileAsync(address, info.Crc, info.Offset);

            while (!operation.isDone)
            {
                await Task.Yield();
            }

            AssetBundle assetBundle = operation.assetBundle;

            return assetBundle;
        }
    }
}
