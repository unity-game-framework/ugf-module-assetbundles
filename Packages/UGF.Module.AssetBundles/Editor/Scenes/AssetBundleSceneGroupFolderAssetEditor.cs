using UGF.Assets.Editor;
using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;

namespace UGF.Module.AssetBundles.Editor.Scenes
{
    [CustomEditor(typeof(AssetBundleSceneGroupFolderAsset), true)]
    internal class AssetBundleSceneGroupFolderAssetEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyFolder;
        private SerializedProperty m_propertyGroup;
        private EditorObjectReferenceDrawer m_drawerGroup;

        private void OnEnable()
        {
            m_propertyFolder = serializedObject.FindProperty("m_folder");
            m_propertyGroup = serializedObject.FindProperty("m_group");

            m_drawerGroup = new EditorObjectReferenceDrawer(m_propertyGroup)
            {
                Drawer = { DisplayTitlebar = true }
            };

            m_drawerGroup.Enable();
        }

        private void OnDisable()
        {
            m_drawerGroup.Disable();
        }

        public override void OnInspectorGUI()
        {
            using (new SerializedObjectUpdateScope(serializedObject))
            {
                EditorIMGUIUtility.DrawScriptProperty(serializedObject);

                EditorGUILayout.PropertyField(m_propertyFolder);
                EditorGUILayout.PropertyField(m_propertyGroup);
            }

            AssetFolderEditorGUIUtility.DrawControlsGUILayout(serializedObject);

            m_drawerGroup.DrawGUILayout();
        }
    }
}
