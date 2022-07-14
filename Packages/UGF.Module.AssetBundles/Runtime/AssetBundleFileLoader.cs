using System;
using System.Threading.Tasks;
using UGF.Application.Runtime;
using UGF.EditorTools.Runtime.Ids;
using UGF.RuntimeTools.Runtime.Contexts;
using UnityEngine;

namespace UGF.Module.AssetBundles.Runtime
{
    public class AssetBundleFileLoader : AssetBundleLoader<AssetBundleFileInfo, AssetBundleLoadParameters, AssetBundleUnloadParameters>
    {
        public AssetBundleFileLoader() : base(AssetBundleLoadParameters.Default, AssetBundleUnloadParameters.Default)
        {
        }

        public AssetBundleFileLoader(AssetBundleLoadParameters defaultLoadParameters, AssetBundleUnloadParameters defaultUnloadParameters) : base(defaultLoadParameters, defaultUnloadParameters)
        {
        }

        protected override AssetBundle OnLoadAssetBundle(AssetBundleFileInfo info, GlobalId id, Type type, AssetBundleLoadParameters parameters, IContext context)
        {
            var application = context.Get<IApplication>();
            var assetBundleModule = application.GetModule<AssetBundleModule>();
            IAssetBundleStorage storage = assetBundleModule.Storages.Get(info.StorageId);
            string address = storage.GetAddress(info, id, type, parameters, context);

            AssetBundle assetBundle = AssetBundle.LoadFromFile(address, info.Crc, info.Offset);

            return assetBundle ? assetBundle : throw new NullReferenceException($"AssetBundle load result is null by the specified arguments: id:'{id}', address:'{address}'.");
        }

        protected override async Task<AssetBundle> OnLoadAssetBundleAsync(AssetBundleFileInfo info, GlobalId id, Type type, AssetBundleLoadParameters parameters, IContext context)
        {
            var application = context.Get<IApplication>();
            var assetBundleModule = application.GetModule<AssetBundleModule>();
            IAssetBundleStorage storage = assetBundleModule.Storages.Get(info.StorageId);
            string address = storage.GetAddress(info, id, type, parameters, context);

            AssetBundleCreateRequest operation = AssetBundle.LoadFromFileAsync(address, info.Crc, info.Offset);

            while (!operation.isDone)
            {
                await Task.Yield();
            }

            AssetBundle assetBundle = operation.assetBundle;

            return assetBundle ? assetBundle : throw new NullReferenceException($"AssetBundle load result is null by the specified arguments: id:'{id}', address:'{address}'.");
        }
    }
}
