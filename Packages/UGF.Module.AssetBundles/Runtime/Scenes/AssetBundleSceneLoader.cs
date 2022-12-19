using System.Threading.Tasks;
using UGF.Application.Runtime;
using UGF.EditorTools.Runtime.Ids;
using UGF.Module.Assets.Runtime;
using UGF.Module.Scenes.Runtime;
using UGF.Module.Scenes.Runtime.Loaders.Manager;
using UGF.RuntimeTools.Runtime.Contexts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UGF.Module.AssetBundles.Runtime.Scenes
{
    public class AssetBundleSceneLoader : ManagerSceneLoader
    {
        public AssetBundleSceneLoader()
        {
        }

        public AssetBundleSceneLoader(ISceneLoadParameters defaultLoadParameters, ISceneUnloadParameters defaultUnloadParameters) : base(defaultLoadParameters, defaultUnloadParameters)
        {
        }

        protected override Scene OnLoad(GlobalId id, ISceneInfo info, ISceneLoadParameters parameters, IContext context)
        {
            var assetModule = context.Get<IApplication>().GetModule<IAssetModule>();
            var assetBundleSceneInfo = (AssetBundleSceneInfo)info;

            assetModule.Load<AssetBundle>(assetBundleSceneInfo.AssetBundleId);

            return base.OnLoad(id, info, parameters, context);
        }

        protected override async Task<Scene> OnLoadAsync(GlobalId id, ISceneInfo info, ISceneLoadParameters parameters, IContext context)
        {
            var assetModule = context.Get<IApplication>().GetModule<IAssetModule>();
            var assetBundleSceneInfo = (AssetBundleSceneInfo)info;

            await assetModule.LoadAsync<AssetBundle>(assetBundleSceneInfo.AssetBundleId);

            return await base.OnLoadAsync(id, info, parameters, context);
        }

        protected override void OnUnload(GlobalId id, Scene scene, ISceneInfo info, ISceneUnloadParameters parameters, IContext context)
        {
            var assetModule = context.Get<IApplication>().GetModule<IAssetModule>();
            var assetBundleSceneInfo = (AssetBundleSceneInfo)info;
            var assetBundle = (AssetBundle)assetModule.Tracker.Get(assetBundleSceneInfo.AssetBundleId).Asset;

            base.OnUnload(id, scene, info, parameters, context);

            assetModule.Unload(assetBundleSceneInfo.AssetBundleId, assetBundle);
        }

        protected override async Task OnUnloadAsync(GlobalId id, Scene scene, ISceneInfo info, ISceneUnloadParameters parameters, IContext context)
        {
            var assetModule = context.Get<IApplication>().GetModule<IAssetModule>();
            var assetBundleSceneInfo = (AssetBundleSceneInfo)info;
            var assetBundle = (AssetBundle)assetModule.Tracker.Get(assetBundleSceneInfo.AssetBundleId).Asset;

            await base.OnUnloadAsync(id, scene, info, parameters, context);
            await assetModule.UnloadAsync(assetBundleSceneInfo.AssetBundleId, assetBundle);
        }
    }
}
