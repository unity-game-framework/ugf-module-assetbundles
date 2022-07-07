using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;
using UnityEngine;

namespace UGF.Module.AssetBundles.Editor
{
    [CustomEditor(typeof(AssetBundleEditorSettingsData), true)]
    internal class AssetBundleEditorSettingsDataEditor : UnityEditor.Editor
    {
        private ReorderableListDrawer m_listBuilds;
        private ReorderableListSelectionDrawerByElement m_listBuildsSelection;

        private void OnEnable()
        {
            m_listBuilds = new ReorderableListDrawer(serializedObject.FindProperty("m_builds"));

            m_listBuildsSelection = new ReorderableListSelectionDrawerByElement(m_listBuilds)
            {
                Drawer =
                {
                    DisplayTitlebar = true
                }
            };

            m_listBuilds.Enable();
            m_listBuildsSelection.Enable();
        }

        private void OnDisable()
        {
            m_listBuildsSelection.Disable();
            m_listBuilds.Disable();
        }

        public override void OnInspectorGUI()
        {
            using (new SerializedObjectUpdateScope(serializedObject))
            {
                m_listBuilds.DrawGUILayout();
            }

            EditorGUILayout.Space();

            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.FlexibleSpace();

                using (new EditorGUI.DisabledScope(m_listBuilds.SerializedProperty.arraySize == 0))
                {
                    if (GUILayout.Button("Clear All", GUILayout.Width(75F)))
                    {
                        OnClear();
                    }

                    if (GUILayout.Button("Build All", GUILayout.Width(75F)))
                    {
                        OnBuild();
                    }
                }

                EditorGUILayout.Space();
            }

            EditorGUILayout.Space();
            EditorGUILayout.Space();

            m_listBuildsSelection.DrawGUILayout();
        }

        private void OnClear()
        {
        }

        private void OnBuild()
        {
        }
    }
}
