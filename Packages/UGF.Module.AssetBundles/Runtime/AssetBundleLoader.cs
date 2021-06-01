using System;
using System.Threading.Tasks;
using UGF.Application.Runtime;
using UGF.Module.Assets.Runtime;
using UGF.RuntimeTools.Runtime.Contexts;
using UnityEngine;

namespace UGF.Module.AssetBundles.Runtime
{
    public class AssetBundleLoader : AssetLoader<AssetBundleInfo, AssetBundleLoadParameters, AssetBundleUnloadParameters>
    {
        protected override object OnLoad(AssetBundleInfo info, string id, Type type, AssetBundleLoadParameters parameters, IContext context)
        {
            var application = context.Get<IApplication>();
            var assetModule = application.GetModule<IAssetModule>();

            for (int i = 0; i < info.Dependencies.Count; i++)
            {
                string dependency = info.Dependencies[i];

                assetModule.Load(dependency, typeof(AssetBundle), parameters);
            }

            AssetBundle assetBundle = AssetBundle.LoadFromFile(info.Address);

            return assetBundle;
        }

        protected override async Task<object> OnLoadAsync(AssetBundleInfo info, string id, Type type, AssetBundleLoadParameters parameters, IContext context)
        {
            var application = context.Get<IApplication>();
            var assetModule = application.GetModule<IAssetModule>();

            for (int i = 0; i < info.Dependencies.Count; i++)
            {
                string dependency = info.Dependencies[i];

                await assetModule.LoadAsync(dependency, typeof(AssetBundle), parameters);
            }

            AssetBundleCreateRequest operation = AssetBundle.LoadFromFileAsync(info.Address);

            while (!operation.isDone)
            {
                await Task.Yield();
            }

            AssetBundle assetBundle = operation.assetBundle;

            return assetBundle;
        }

        protected override void OnUnload(AssetBundleInfo info, string id, object asset, AssetBundleUnloadParameters parameters, IContext context)
        {
            var application = context.Get<IApplication>();
            var assetModule = application.GetModule<IAssetModule>();
            var assetBundle = (AssetBundle)asset;

            assetBundle.Unload(parameters.UnloadAllLoadedObjects);

            for (int i = 0; i < info.Dependencies.Count; i++)
            {
                string dependency = info.Dependencies[i];
                AssetTrack track = assetModule.Tracker.Get(dependency);

                assetModule.Unload(dependency, track.Asset, parameters);
            }
        }

        protected override async Task OnUnloadAsync(AssetBundleInfo info, string id, object asset, AssetBundleUnloadParameters parameters, IContext context)
        {
            var application = context.Get<IApplication>();
            var assetModule = application.GetModule<IAssetModule>();
            var assetBundle = (AssetBundle)asset;

            AsyncOperation operation = assetBundle.UnloadAsync(parameters.UnloadAllLoadedObjects);

            while (!operation.isDone)
            {
                await Task.Yield();
            }

            for (int i = 0; i < info.Dependencies.Count; i++)
            {
                string dependency = info.Dependencies[i];
                AssetTrack track = assetModule.Tracker.Get(dependency);

                await assetModule.UnloadAsync(dependency, track.Asset, parameters);
            }
        }
    }
}
