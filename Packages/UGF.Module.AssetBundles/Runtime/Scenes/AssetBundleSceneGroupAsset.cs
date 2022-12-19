using System.Collections.Generic;
using UGF.EditorTools.Runtime.Assets;
using UGF.EditorTools.Runtime.Ids;
using UGF.Module.Scenes.Runtime;
using UGF.Module.Scenes.Runtime.Loaders.Manager;
using UnityEngine;

namespace UGF.Module.AssetBundles.Runtime.Scenes
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Assets/Asset Bundle Scene Group", order = 2000)]
    public class AssetBundleSceneGroupAsset : ManagerSceneGroupAsset
    {
        [AssetId(typeof(AssetBundleAsset))]
        [SerializeField] private GlobalId m_assetBundle;

        public GlobalId AssetBundle { get { return m_assetBundle; } set { m_assetBundle = value; } }

        protected override void OnGetScenes(IDictionary<GlobalId, ISceneInfo> scenes)
        {
            for (int i = 0; i < Scenes.Count; i++)
            {
                SceneReference reference = Scenes[i];

                scenes.Add(reference.Guid, new AssetBundleSceneInfo(Loader, m_assetBundle, reference.Path));
            }
        }
    }
}
