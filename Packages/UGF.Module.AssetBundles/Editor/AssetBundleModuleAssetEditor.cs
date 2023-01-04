using UGF.EditorTools.Editor.Assets;
using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UGF.Module.AssetBundles.Runtime;
using UnityEditor;

namespace UGF.Module.AssetBundles.Editor
{
    [CustomEditor(typeof(AssetBundleModuleAsset), true)]
    internal class AssetBundleModuleAssetEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyUnloadTrackedAssetBundlesOnUninitialize;
        private AssetIdReferenceListDrawer m_listStorages;
        private ReorderableListSelectionDrawerByPath m_listStoragesSelection;
        private AssetIdReferenceListDrawer m_listAssetBundles;
        private ReorderableListSelectionDrawerByPath m_listAssetBundlesSelection;

        private void OnEnable()
        {
            m_propertyUnloadTrackedAssetBundlesOnUninitialize = serializedObject.FindProperty("m_unloadTrackedAssetBundlesOnUninitialize");

            m_listStorages = new AssetIdReferenceListDrawer(serializedObject.FindProperty("m_storages"))
            {
                DisplayAsSingleLine = true
            };

            m_listStoragesSelection = new ReorderableListSelectionDrawerByPath(m_listStorages, "m_asset")
            {
                Drawer = { DisplayTitlebar = true }
            };

            m_listAssetBundles = new AssetIdReferenceListDrawer(serializedObject.FindProperty("m_assetBundles"))
            {
                DisplayAsSingleLine = true
            };

            m_listAssetBundlesSelection = new ReorderableListSelectionDrawerByPath(m_listAssetBundles, "m_asset")
            {
                Drawer = { DisplayTitlebar = true }
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

                EditorGUILayout.PropertyField(m_propertyUnloadTrackedAssetBundlesOnUninitialize);

                m_listStorages.DrawGUILayout();
                m_listAssetBundles.DrawGUILayout();

                m_listStoragesSelection.DrawGUILayout();
                m_listAssetBundlesSelection.DrawGUILayout();
            }
        }
    }
}
