﻿using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UGF.Module.AssetBundles.Runtime;
using UnityEditor;

namespace UGF.Module.AssetBundles.Editor
{
    [CustomEditor(typeof(AssetBundleFileAsset), true)]
    internal class AssetBundleFileAssetEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyLoader;
        private SerializedProperty m_propertyStorage;
        private SerializedProperty m_propertyCrc;
        private SerializedProperty m_propertyOffset;
        private ReorderableListDrawer m_listDependencies;
        private ReorderableListSelectionDrawerByElementGlobalId m_listDependenciesSelection;

        private void OnEnable()
        {
            m_propertyLoader = serializedObject.FindProperty("m_loader");
            m_propertyStorage = serializedObject.FindProperty("m_storage");
            m_propertyCrc = serializedObject.FindProperty("m_crc");
            m_propertyOffset = serializedObject.FindProperty("m_offset");

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
                EditorGUILayout.PropertyField(m_propertyStorage);
                EditorGUILayout.PropertyField(m_propertyCrc);
                EditorGUILayout.PropertyField(m_propertyOffset);

                m_listDependencies.DrawGUILayout();
                m_listDependenciesSelection.DrawGUILayout();
            }
        }
    }
}
