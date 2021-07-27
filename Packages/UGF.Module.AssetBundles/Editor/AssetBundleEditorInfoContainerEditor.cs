using System.IO;
using UGF.EditorTools.Editor.IMGUI;
using UnityEditor;
using UnityEngine;

namespace UGF.Module.AssetBundles.Editor
{
    [CustomEditor(typeof(AssetBundleEditorInfoContainer), true)]
    internal class AssetBundleEditorInfoContainerEditor : UnityEditor.Editor
    {
        private readonly EditorDrawer m_debugDrawer = new EditorDrawer();
        private SerializedProperty m_propertyDebug;
        private SerializedProperty m_propertyPath;
        private SerializedProperty m_propertyName;
        private SerializedProperty m_propertyCrc;
        private SerializedProperty m_propertyIsStreamedSceneAssetBundle;
        private SerializedProperty m_propertySize;
        private ReorderableListDrawer m_listAssets;
        private ReorderableListDrawer m_listDependencies;

        private void OnEnable()
        {
            m_propertyDebug = serializedObject.FindProperty("m_debug");
            m_propertyPath = serializedObject.FindProperty("m_path");
            m_propertyName = serializedObject.FindProperty("m_name");
            m_propertyCrc = serializedObject.FindProperty("m_crc");
            m_propertyIsStreamedSceneAssetBundle = serializedObject.FindProperty("m_isStreamedSceneAssetBundle");
            m_propertySize = serializedObject.FindProperty("m_size");
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
            m_debugDrawer.Enable();
        }

        private void OnDisable()
        {
            m_listAssets.Disable();
            m_listDependencies.Disable();

            DebugDrawerClear();

            m_debugDrawer.Disable();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DebugDrawerCheck();

            if (!m_propertyDebug.boolValue)
            {
                EditorGUILayout.PropertyField(m_propertyPath);
                EditorGUILayout.PropertyField(m_propertyName);
                EditorGUILayout.PropertyField(m_propertyCrc);
                EditorGUILayout.PropertyField(m_propertyIsStreamedSceneAssetBundle);
                EditorGUILayout.TextField(m_propertySize.displayName, EditorUtility.FormatBytes(m_propertySize.longValue));

                m_listAssets.DrawGUILayout();
                m_listDependencies.DrawGUILayout();
            }
            else
            {
                if (m_debugDrawer.HasEditor)
                {
                    m_debugDrawer.DrawGUILayout();
                }
                else
                {
                    EditorGUILayout.Space();
                    EditorGUILayout.HelpBox($"Asset Bundle file not found at the specific path: {m_propertyPath.stringValue}", MessageType.Info);
                }
            }
        }

        private void DebugDrawerCheck()
        {
            if (m_propertyDebug.boolValue)
            {
                if (!m_debugDrawer.HasEditor)
                {
                    DebugDrawerCreate();
                }
            }
            else
            {
                DebugDrawerClear();
            }
        }

        private void DebugDrawerCreate()
        {
            string path = m_propertyPath.stringValue;

            if (File.Exists(path))
            {
                AssetBundle assetBundle = AssetBundle.LoadFromFile(path);

                m_debugDrawer.Set(assetBundle);
            }
        }

        private void DebugDrawerClear()
        {
            if (m_debugDrawer.HasEditor)
            {
                var assetBundle = (AssetBundle)m_debugDrawer.Editor.target;

                m_debugDrawer.Clear();

                assetBundle.Unload(true);
            }
        }
    }
}
