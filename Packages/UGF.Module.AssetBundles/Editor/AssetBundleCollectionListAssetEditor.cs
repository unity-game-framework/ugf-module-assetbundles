using UGF.EditorTools.Editor.Assets;
using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UGF.Module.AssetBundles.Runtime;
using UnityEditor;

namespace UGF.Module.AssetBundles.Editor
{
    [CustomEditor(typeof(AssetBundleCollectionListAsset), true)]
    internal class AssetBundleCollectionListAssetEditor : UnityEditor.Editor
    {
        private AssetIdReferenceListDrawer m_listAssetBundles;
        private ReorderableListSelectionDrawerByPath m_listAssetBundlesSelection;

        private void OnEnable()
        {
            m_listAssetBundles = new AssetIdReferenceListDrawer(serializedObject.FindProperty("m_assetBundles"));

            m_listAssetBundlesSelection = new ReorderableListSelectionDrawerByPath(m_listAssetBundles, "m_asset")
            {
                Drawer = { DisplayTitlebar = true }
            };

            m_listAssetBundles.Enable();
            m_listAssetBundlesSelection.Enable();
        }

        private void OnDisable()
        {
            m_listAssetBundles.Disable();
            m_listAssetBundlesSelection.Disable();
        }

        public override void OnInspectorGUI()
        {
            using (new SerializedObjectUpdateScope(serializedObject))
            {
                EditorIMGUIUtility.DrawScriptProperty(serializedObject);

                m_listAssetBundles.DrawGUILayout();
                m_listAssetBundlesSelection.DrawGUILayout();
            }
        }
    }
}
