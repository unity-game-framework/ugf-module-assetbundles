using System;
using System.Threading.Tasks;
using UGF.Application.Runtime;
using UGF.Module.Assets.Runtime;
using UGF.RuntimeTools.Runtime.Contexts;
using UnityEngine;

namespace UGF.Module.AssetBundles.Runtime
{
    public abstract class AssetBundleLoader<TInfo, TLoadParameters, TUnloadParameters> : AssetLoader<TInfo, TLoadParameters, TUnloadParameters>
        where TInfo : AssetBundleInfo
        where TLoadParameters : class, IAssetLoadParameters
        where TUnloadParameters : AssetBundleUnloadParameters
    {
        protected AssetBundleLoader(TLoadParameters defaultLoadParameters, TUnloadParameters defaultUnloadParameters) : base(defaultLoadParameters, defaultUnloadParameters)
        {
        }

        protected override object OnLoad(TInfo info, string id, Type type, TLoadParameters parameters, IContext context)
        {
            OnLoadDependencies(info, id, type, parameters, context);

            AssetBundle assetBundle = OnLoadAssetBundle(info, id, type, parameters, context);

            return assetBundle;
        }

        protected override async Task<object> OnLoadAsync(TInfo info, string id, Type type, TLoadParameters parameters, IContext context)
        {
            await OnLoadDependenciesAsync(info, id, type, parameters, context);

            AssetBundle assetBundle = await OnLoadAssetBundleAsync(info, id, type, parameters, context);

            return assetBundle;
        }

        protected override void OnUnload(TInfo info, string id, object asset, TUnloadParameters parameters, IContext context)
        {
            OnUnloadAssetBundle(info, id, asset, parameters, context);
            OnUnloadDependencies(info, id, asset, parameters, context);
        }

        protected override async Task OnUnloadAsync(TInfo info, string id, object asset, TUnloadParameters parameters, IContext context)
        {
            await OnUnloadAssetBundleAsync(info, id, asset, parameters, context);
            await OnUnloadDependenciesAsync(info, id, asset, parameters, context);
        }

        protected abstract AssetBundle OnLoadAssetBundle(TInfo info, string id, Type type, TLoadParameters parameters, IContext context);
        protected abstract Task<AssetBundle> OnLoadAssetBundleAsync(TInfo info, string id, Type type, TLoadParameters parameters, IContext context);

        protected virtual void OnUnloadAssetBundle(TInfo info, string id, object asset, TUnloadParameters parameters, IContext context)
        {
            var assetBundle = (AssetBundle)asset;

            assetBundle.Unload(parameters.UnloadAllLoadedObjects);
        }

        protected virtual async Task OnUnloadAssetBundleAsync(TInfo info, string id, object asset, TUnloadParameters parameters, IContext context)
        {
            var assetBundle = (AssetBundle)asset;

            AsyncOperation operation = assetBundle.UnloadAsync(parameters.UnloadAllLoadedObjects);

            while (!operation.isDone)
            {
                await Task.Yield();
            }
        }

        protected virtual void OnLoadDependencies(TInfo info, string id, Type type, TLoadParameters parameters, IContext context)
        {
            var application = context.Get<IApplication>();
            var assetModule = application.GetModule<IAssetModule>();

            for (int i = 0; i < info.Dependencies.Count; i++)
            {
                string dependency = info.Dependencies[i];

                assetModule.Load(dependency, typeof(AssetBundle), parameters);
            }
        }

        protected virtual async Task OnLoadDependenciesAsync(TInfo info, string id, Type type, TLoadParameters parameters, IContext context)
        {
            var application = context.Get<IApplication>();
            var assetModule = application.GetModule<IAssetModule>();

            for (int i = 0; i < info.Dependencies.Count; i++)
            {
                string dependency = info.Dependencies[i];

                await assetModule.LoadAsync(dependency, typeof(AssetBundle), parameters);
            }
        }

        protected virtual void OnUnloadDependencies(TInfo info, string id, object asset, TUnloadParameters parameters, IContext context)
        {
            var application = context.Get<IApplication>();
            var assetModule = application.GetModule<IAssetModule>();

            for (int i = 0; i < info.Dependencies.Count; i++)
            {
                string dependency = info.Dependencies[i];

                if (assetModule.Tracker.TryGet(dependency, out AssetTrack track))
                {
                    assetModule.Unload(dependency, track.Asset, parameters);
                }
            }
        }

        protected virtual async Task OnUnloadDependenciesAsync(TInfo info, string id, object asset, TUnloadParameters parameters, IContext context)
        {
            var application = context.Get<IApplication>();
            var assetModule = application.GetModule<IAssetModule>();

            for (int i = 0; i < info.Dependencies.Count; i++)
            {
                string dependency = info.Dependencies[i];

                if (assetModule.Tracker.TryGet(dependency, out AssetTrack track))
                {
                    await assetModule.UnloadAsync(dependency, track.Asset, parameters);
                }
            }
        }
    }
}
