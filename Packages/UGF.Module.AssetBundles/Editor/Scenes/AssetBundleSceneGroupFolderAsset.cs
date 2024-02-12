using UGF.Assets.Editor;
using UGF.EditorTools.Runtime.Ids;
using UGF.Module.AssetBundles.Runtime.Scenes;
using UGF.Module.Scenes.Runtime;
using UnityEditor;
using UnityEngine;

namespace UGF.Module.AssetBundles.Editor.Scenes
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Assets/Asset Bundle Scene Group Folder", order = 2000)]
    public class AssetBundleSceneGroupFolderAsset : AssetFolderAsset<SceneAsset>
    {
        [SerializeField] private AssetBundleSceneGroupAsset m_group;

        public AssetBundleSceneGroupAsset Group { get { return m_group; } set { m_group = value; } }

        protected override bool OnIsValid()
        {
            return m_group != null;
        }

        protected override void OnUpdate()
        {
            m_group.Scenes.Clear();

            string[] guids = FindAssetsAsGuids();

            for (int i = 0; i < guids.Length; i++)
            {
                string guid = guids[i];

                var id = new GlobalId(guid);
                string path = AssetDatabase.GUIDToAssetPath(guid);

                m_group.Scenes.Add(new SceneReference(id, path));
            }

            EditorUtility.SetDirty(m_group);
        }
    }
}
