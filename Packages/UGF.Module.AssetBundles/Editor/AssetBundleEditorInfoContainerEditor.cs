using UGF.EditorTools.Editor.IMGUI;
using UnityEditor;

namespace UGF.Module.AssetBundles.Editor
{
    [CustomEditor(typeof(AssetBundleEditorInfoContainer), true)]
    internal class AssetBundleEditorInfoContainerEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyName;
        private SerializedProperty m_propertyCrc;
        private SerializedProperty m_propertyIsStreamedSceneAssetBundle;
        private ReorderableListDrawer m_listAssets;
        private ReorderableListDrawer m_listDependencies;

        private void OnEnable()
        {
            m_propertyName = serializedObject.FindProperty("m_name");
            m_propertyCrc = serializedObject.FindProperty("m_crc");
            m_propertyIsStreamedSceneAssetBundle = serializedObject.FindProperty("m_isStreamedSceneAssetBundle");
            m_listAssets = new ReorderableListDrawer(serializedObject.FindProperty("m_assets"));
            m_listDependencies = new ReorderableListDrawer(serializedObject.FindProperty("m_dependencies"));

            m_listAssets.List.displayAdd = false;
            m_listAssets.List.displayRemove = false;
            m_listAssets.List.draggable = false;
            m_listDependencies.List.displayAdd = false;
            m_listDependencies.List.displayRemove = false;
            m_listDependencies.List.draggable = false;

            m_listAssets.Enable();
            m_listDependencies.Enable();
        }

        private void OnDisable()
        {
            m_listAssets.Disable();
            m_listDependencies.Disable();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            if (!string.IsNullOrEmpty(m_propertyName.stringValue))
            {
                EditorGUILayout.PropertyField(m_propertyName);
                EditorGUILayout.PropertyField(m_propertyCrc);
                EditorGUILayout.PropertyField(m_propertyIsStreamedSceneAssetBundle);

                m_listAssets.DrawGUILayout();
                m_listDependencies.DrawGUILayout();
            }
            else
            {
                EditorGUILayout.Space();
                EditorGUILayout.HelpBox("No Asset Bundle found, build required.", MessageType.Info);
            }
        }
    }
}
