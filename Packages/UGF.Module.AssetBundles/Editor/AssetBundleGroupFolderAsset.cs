using UGF.EditorTools.Runtime.Ids;
using UGF.Module.AssetBundles.Runtime;
using UGF.Module.Assets.Editor;
using UnityEditor;
using UnityEngine;

namespace UGF.Module.AssetBundles.Editor
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Assets/Asset Bundle Group Folder", order = 2000)]
    public class AssetBundleGroupFolderAsset : AssetFolderAsset
    {
        [SerializeField] private AssetBundleGroupAsset m_group;

        public AssetBundleGroupAsset Group { get { return m_group; } set { m_group = value; } }

        protected override bool OnIsValid()
        {
            return m_group != null;
        }

        protected override void OnUpdate()
        {
            m_group.Assets.Clear();

            string[] guids = FindAssetsAsGuids();

            for (int i = 0; i < guids.Length; i++)
            {
                string guid = guids[i];

                m_group.Assets.Add(new GlobalId(guid));
            }

            EditorUtility.SetDirty(m_group);
        }
    }
}
