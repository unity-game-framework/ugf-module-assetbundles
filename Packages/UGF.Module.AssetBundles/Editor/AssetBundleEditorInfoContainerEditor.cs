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
        private ReorderableListDrawer m_listAssetNames;
        private ReorderableListDrawer m_listDependencies;

        private void OnEnable()
        {
            m_propertyName = serializedObject.FindProperty("m_name");
            m_propertyCrc = serializedObject.FindProperty("m_crc");
            m_propertyIsStreamedSceneAssetBundle = serializedObject.FindProperty("m_isStreamedSceneAssetBundle");
            m_listAssetNames = new ReorderableListDrawer(serializedObject.FindProperty("m_assetNames"));
            m_listDependencies = new ReorderableListDrawer(serializedObject.FindProperty("m_dependencies"));

            m_listAssetNames.List.displayAdd = false;
            m_listAssetNames.List.displayRemove = false;
            m_listAssetNames.List.draggable = false;
            m_listDependencies.List.displayAdd = false;
            m_listDependencies.List.displayRemove = false;
            m_listDependencies.List.draggable = false;

            m_listAssetNames.Enable();
            m_listDependencies.Enable();
        }

        private void OnDisable()
        {
            m_listAssetNames.Disable();
            m_listDependencies.Disable();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(m_propertyName);
            EditorGUILayout.PropertyField(m_propertyCrc);
            EditorGUILayout.PropertyField(m_propertyIsStreamedSceneAssetBundle);

            m_listAssetNames.DrawGUILayout();
            m_listDependencies.DrawGUILayout();
        }
    }
}
