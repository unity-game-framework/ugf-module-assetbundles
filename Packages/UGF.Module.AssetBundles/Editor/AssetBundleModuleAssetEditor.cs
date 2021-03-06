using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UGF.Module.AssetBundles.Runtime;
using UnityEditor;

namespace UGF.Module.AssetBundles.Editor
{
    [CustomEditor(typeof(AssetBundleModuleAsset), true)]
    internal class AssetBundleModuleAssetEditor : UnityEditor.Editor
    {
        private ReorderableListDrawer m_listStorages;
        private ReorderableListSelectionDrawerByPath m_listStoragesSelection;
        private ReorderableListDrawer m_listAssetBundles;
        private ReorderableListSelectionDrawerByPath m_listAssetBundlesSelection;

        private void OnEnable()
        {
            m_listStorages = new ReorderableListDrawer(serializedObject.FindProperty("m_storages"))
            {
                DisplayAsSingleLine = true
            };

            m_listStoragesSelection = new ReorderableListSelectionDrawerByPath(m_listStorages, "m_asset")
            {
                Drawer =
                {
                    DisplayTitlebar = true
                }
            };

            m_listAssetBundles = new ReorderableListDrawer(serializedObject.FindProperty("m_assetBundles"))
            {
                DisplayAsSingleLine = true
            };

            m_listAssetBundlesSelection = new ReorderableListSelectionDrawerByPath(m_listAssetBundles, "m_asset")
            {
                Drawer =
                {
                    DisplayTitlebar = true
                }
            };

            m_listStorages.Enable();
            m_listStoragesSelection.Enable();
            m_listAssetBundles.Enable();
            m_listAssetBundlesSelection.Enable();
        }

        private void OnDisable()
        {
            m_listStorages.Disable();
            m_listStoragesSelection.Disable();
            m_listAssetBundles.Disable();
            m_listAssetBundlesSelection.Disable();
        }

        public override void OnInspectorGUI()
        {
            using (new SerializedObjectUpdateScope(serializedObject))
            {
                EditorIMGUIUtility.DrawScriptProperty(serializedObject);

                m_listStorages.DrawGUILayout();
                m_listAssetBundles.DrawGUILayout();

                m_listStoragesSelection.DrawGUILayout();
                m_listAssetBundlesSelection.DrawGUILayout();
            }
        }
    }
}
