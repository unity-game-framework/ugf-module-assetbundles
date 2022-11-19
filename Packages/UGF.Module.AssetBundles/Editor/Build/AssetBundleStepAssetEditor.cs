using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;

namespace UGF.Module.AssetBundles.Editor.Build
{
    [CustomEditor(typeof(AssetBundleStepAsset), true)]
    internal class AssetBundleStepAssetEditor : UnityEditor.Editor
    {
        private ReorderableListDrawer m_listBuilds;
        private ReorderableListSelectionDrawerByElement m_listBuildsSelection;

        private void OnEnable()
        {
            m_listBuilds = new ReorderableListDrawer(serializedObject.FindProperty("m_builds"));

            m_listBuildsSelection = new ReorderableListSelectionDrawerByElement(m_listBuilds)
            {
                Drawer = { DisplayTitlebar = true }
            };

            m_listBuilds.Enable();
            m_listBuildsSelection.Enable();
        }

        private void OnDisable()
        {
            m_listBuilds.Disable();
            m_listBuildsSelection.Disable();
        }

        public override void OnInspectorGUI()
        {
            using (new SerializedObjectUpdateScope(serializedObject))
            {
                EditorIMGUIUtility.DrawScriptProperty(serializedObject);

                m_listBuilds.DrawGUILayout();
                m_listBuildsSelection.DrawGUILayout();
            }
        }
    }
}
