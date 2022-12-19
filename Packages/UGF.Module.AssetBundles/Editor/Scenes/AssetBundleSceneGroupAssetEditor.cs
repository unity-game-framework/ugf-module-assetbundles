using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UGF.Module.AssetBundles.Runtime.Scenes;
using UGF.Module.Scenes.Editor;
using UGF.Module.Scenes.Editor.Loaders.Manager;
using UnityEditor;
using UnityEngine;

namespace UGF.Module.AssetBundles.Editor.Scenes
{
    [CustomEditor(typeof(AssetBundleSceneGroupAsset), true)]
    internal class AssetBundleSceneGroupAssetEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyLoader;
        private SerializedProperty m_propertyAssetBundle;
        private ReorderableListDrawer m_listScenes;

        private void OnEnable()
        {
            m_propertyLoader = serializedObject.FindProperty("m_loader");
            m_propertyAssetBundle = serializedObject.FindProperty("m_assetBundle");

            m_listScenes = new ReorderableListDrawer(serializedObject.FindProperty("m_scenes"))
            {
                DisplayAsSingleLine = true
            };

            m_listScenes.Enable();
        }

        private void OnDisable()
        {
            m_listScenes.Disable();
        }

        public override void OnInspectorGUI()
        {
            using (new SerializedObjectUpdateScope(serializedObject))
            {
                EditorIMGUIUtility.DrawScriptProperty(serializedObject);

                EditorGUILayout.PropertyField(m_propertyLoader);
                EditorGUILayout.PropertyField(m_propertyAssetBundle);

                m_listScenes.DrawGUILayout();
            }

            EditorGUILayout.Space();

            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.FlexibleSpace();

                if (GUILayout.Button("Refresh All", GUILayout.Width(75F)))
                {
                    ManagerSceneEditorUtility.UpdateSceneGroupAll();
                }

                if (GUILayout.Button("Refresh", GUILayout.Width(75F)))
                {
                    SceneReferenceEditorUtility.UpdateScenePathAll(m_listScenes.SerializedProperty);

                    serializedObject.ApplyModifiedProperties();
                }
            }

            if (!SceneReferenceEditorUtility.ValidateReferencesAll(m_listScenes.SerializedProperty))
            {
                EditorGUILayout.Space();
                EditorGUILayout.HelpBox("Scene reference collection contains entries with missing or invalid data.", MessageType.Warning);
            }
        }
    }
}
