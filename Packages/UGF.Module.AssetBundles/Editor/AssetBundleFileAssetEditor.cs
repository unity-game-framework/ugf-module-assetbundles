﻿using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UGF.Module.AssetBundles.Runtime;
using UnityEditor;

namespace UGF.Module.AssetBundles.Editor
{
    [CustomEditor(typeof(AssetBundleFileAsset), true)]
    internal class AssetBundleFileAssetEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyScript;
        private SerializedProperty m_propertyLoader;
        private SerializedProperty m_propertyFile;
        private SerializedProperty m_propertyCrc;
        private SerializedProperty m_propertyOffset;
        private ReorderableListDrawer m_listDependencies;

        private void OnEnable()
        {
            m_propertyScript = serializedObject.FindProperty("m_Script");
            m_propertyLoader = serializedObject.FindProperty("m_loader");
            m_propertyFile = serializedObject.FindProperty("m_file");
            m_propertyCrc = serializedObject.FindProperty("m_crc");
            m_propertyOffset = serializedObject.FindProperty("m_offset");
            m_listDependencies = new ReorderableListDrawer(serializedObject.FindProperty("m_dependencies"));

            m_listDependencies.Enable();
        }

        private void OnDisable()
        {
            m_listDependencies.Disable();
        }

        public override void OnInspectorGUI()
        {
            using (new SerializedObjectUpdateScope(serializedObject))
            {
                using (new EditorGUI.DisabledScope(true))
                {
                    EditorGUILayout.PropertyField(m_propertyScript);
                }

                EditorGUILayout.PropertyField(m_propertyLoader);
                EditorGUILayout.PropertyField(m_propertyFile);
                EditorGUILayout.PropertyField(m_propertyCrc);
                EditorGUILayout.PropertyField(m_propertyOffset);

                m_listDependencies.DrawGUILayout();
            }
        }
    }
}
