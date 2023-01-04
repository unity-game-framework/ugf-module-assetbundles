using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UGF.Module.AssetBundles.Runtime;
using UnityEditor;

namespace UGF.Module.AssetBundles.Editor
{
    [CustomEditor(typeof(AssetBundleAsset), true)]
    internal class AssetBundleAssetEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyLoader;
        private SerializedProperty m_propertyCrc;
        private ReorderableListDrawer m_listDependencies;
        private ReorderableListSelectionDrawerByElementGlobalId m_listDependenciesSelection;

        private void OnEnable()
        {
            m_propertyLoader = serializedObject.FindProperty("m_loader");
            m_propertyCrc = serializedObject.FindProperty("m_crc");

            m_listDependencies = new ReorderableListDrawer(serializedObject.FindProperty("m_dependencies"))
            {
                DisplayAsSingleLine = true
            };

            m_listDependenciesSelection = new ReorderableListSelectionDrawerByElementGlobalId(m_listDependencies)
            {
                Drawer = { DisplayTitlebar = true }
            };

            m_listDependencies.Enable();
            m_listDependenciesSelection.Enable();
        }

        private void OnDisable()
        {
            m_listDependencies.Disable();
            m_listDependenciesSelection.Disable();
        }

        public override void OnInspectorGUI()
        {
            using (new SerializedObjectUpdateScope(serializedObject))
            {
                EditorIMGUIUtility.DrawScriptProperty(serializedObject);

                EditorGUILayout.PropertyField(m_propertyLoader);
                EditorGUILayout.PropertyField(m_propertyCrc);

                m_listDependencies.DrawGUILayout();
                m_listDependenciesSelection.DrawGUILayout();
            }
        }
    }
}
