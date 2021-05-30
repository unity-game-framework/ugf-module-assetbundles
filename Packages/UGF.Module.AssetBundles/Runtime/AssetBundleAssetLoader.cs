﻿using System;
using System.Threading.Tasks;
using UGF.Application.Runtime;
using UGF.Module.Assets.Runtime;
using UGF.RuntimeTools.Runtime.Contexts;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UGF.Module.AssetBundles.Runtime
{
    public class AssetBundleAssetLoader : AssetLoader<AssetBundleAssetInfo, AssetBundleAssetLoadParameters, AssetBundleAssetUnloadParameters>
    {
        protected override object OnLoad(AssetBundleAssetInfo info, string id, Type type, AssetBundleAssetLoadParameters parameters, IContext context)
        {
            var application = context.Get<IApplication>();
            var assetModule = application.GetModule<IAssetModule>();
            var assetBundle = assetModule.Load<AssetBundle>(info.AssetBundleId, parameters.AssetBundleLoadParameters);

            Object asset = assetBundle.LoadAsset(info.Address);

            return asset;
        }

        protected override async Task<object> OnLoadAsync(AssetBundleAssetInfo info, string id, Type type, AssetBundleAssetLoadParameters parameters, IContext context)
        {
            var application = context.Get<IApplication>();
            var assetModule = application.GetModule<IAssetModule>();
            var assetBundle = await assetModule.LoadAsync<AssetBundle>(info.AssetBundleId, parameters.AssetBundleLoadParameters);

            AssetBundleRequest operation = assetBundle.LoadAssetAsync(info.Address);

            while (!operation.isDone)
            {
                await Task.Yield();
            }

            Object asset = operation.asset;

            return asset;
        }

        protected override void OnUnload(AssetBundleAssetInfo info, string id, object asset, AssetBundleAssetUnloadParameters parameters, IContext context)
        {
            var application = context.Get<IApplication>();
            var assetModule = application.GetModule<IAssetModule>();
            AssetTrack assetTrack = assetModule.Tracker.Get(info.AssetBundleId);
            var assetBundle = (AssetBundle)assetTrack.Asset;

            assetModule.Unload(info.AssetBundleId, assetBundle, parameters.AssetBundleUnloadParameters);
        }

        protected override async Task OnUnloadAsync(AssetBundleAssetInfo info, string id, object asset, AssetBundleAssetUnloadParameters parameters, IContext context)
        {
            var application = context.Get<IApplication>();
            var assetModule = application.GetModule<IAssetModule>();
            AssetTrack assetTrack = assetModule.Tracker.Get(info.AssetBundleId);
            var assetBundle = (AssetBundle)assetTrack.Asset;

            await assetModule.UnloadAsync(info.AssetBundleId, assetBundle, parameters.AssetBundleUnloadParameters);
        }
    }
}
