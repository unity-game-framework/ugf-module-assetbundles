using UGF.Module.Scenes.Runtime;
using UGF.Module.Scenes.Runtime.Loaders.Manager;
using UnityEngine;

namespace UGF.Module.AssetBundles.Runtime.Scenes
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Assets/Asset Bundle Scene Loader", order = 2000)]
    public class AssetBundleSceneLoaderAsset : ManagerSceneLoaderAsset
    {
        protected override ISceneLoader OnBuild()
        {
            return new AssetBundleSceneLoader(DefaultLoadParameters, DefaultUnloadParameters)
            {
                RegisterApplication = RegisterApplication,
                UnloadUnusedAfterUnload = UnloadUnusedAfterUnload
            };
        }
    }
}
