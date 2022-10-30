using System.Collections.Generic;
using UGF.EditorTools.Editor.Ids;
using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UGF.EditorTools.Runtime.Ids;
using UGF.Module.AssetBundles.Runtime;
using UnityEditor;
using UnityEngine;

namespace UGF.Module.AssetBundles.Editor
{
    [CustomEditor(typeof(AssetBundleGroupSharedAsset), true)]
    internal class AssetBundleGroupSharedAssetEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyLoader;
        private SerializedProperty m_propertyAssetBundle;
        private ReorderableListDrawer m_listGroups;
        private ReorderableListDrawer m_listAssets;

        private void OnEnable()
        {
            m_propertyLoader = serializedObject.FindProperty("m_loader");
            m_propertyAssetBundle = serializedObject.FindProperty("m_assetBundle");

            m_listGroups = new ReorderableListDrawer(serializedObject.FindProperty("m_groups"))
            {
                DisplayAsSingleLine = true
            };

            m_listAssets = new ReorderableListDrawer(serializedObject.FindProperty("m_assets"))
            {
                DisplayAsSingleLine = true
            };

            m_listGroups.Enable();
            m_listAssets.Enable();
        }

        private void OnDisable()
        {
            m_listGroups.Disable();
            m_listAssets.Disable();
        }

        public override void OnInspectorGUI()
        {
            using (new SerializedObjectUpdateScope(serializedObject))
            {
                EditorIMGUIUtility.DrawScriptProperty(serializedObject);

                EditorGUILayout.PropertyField(m_propertyLoader);
                EditorGUILayout.PropertyField(m_propertyAssetBundle);

                m_listGroups.DrawGUILayout();
                m_listAssets.DrawGUILayout();
            }

            EditorGUILayout.Space();

            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.FlexibleSpace();

                using (new EditorGUI.DisabledScope(m_listAssets.SerializedProperty.arraySize == 0))
                {
                    if (GUILayout.Button("Clear", GUILayout.Width(75F)))
                    {
                        OnClear();
                    }
                }

                using (new EditorGUI.DisabledScope(m_listGroups.SerializedProperty.arraySize == 0))
                {
                    if (GUILayout.Button("Collect", GUILayout.Width(75F)))
                    {
                        OnCollect();
                    }
                }
            }

            EditorGUILayout.Space();
        }

        private void OnCollect()
        {
            m_listAssets.SerializedProperty.ClearArray();

            var groups = new List<AssetBundleGroupAsset>();
            var ids = new List<GlobalId>();

            for (int i = 0; i < m_listGroups.SerializedProperty.arraySize; i++)
            {
                SerializedProperty propertyElement = m_listGroups.SerializedProperty.GetArrayElementAtIndex(i);
                string guid = GlobalIdEditorUtility.GetGuidFromProperty(propertyElement);
                var group = AssetDatabase.LoadAssetAtPath<AssetBundleGroupAsset>(AssetDatabase.GUIDToAssetPath(guid));

                if (group != null)
                {
                    groups.Add(group);
                }
            }

            AssetBundleBuildEditorUtility.GetDependencies(ids, groups);

            for (int i = 0; i < ids.Count; i++)
            {
                GlobalId id = ids[i];

                m_listAssets.SerializedProperty.InsertArrayElementAtIndex(m_listAssets.SerializedProperty.arraySize);

                SerializedProperty propertyElement = m_listAssets.SerializedProperty.GetArrayElementAtIndex(m_listAssets.SerializedProperty.arraySize - 1);

                GlobalIdEditorUtility.SetGlobalIdToProperty(propertyElement, id);
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void OnClear()
        {
            m_listAssets.SerializedProperty.ClearArray();
            m_listAssets.SerializedProperty.serializedObject.ApplyModifiedProperties();
        }
    }
}
