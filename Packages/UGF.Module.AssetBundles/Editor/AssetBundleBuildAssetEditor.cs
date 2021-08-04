using System.IO;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;
using UnityEngine;

namespace UGF.Module.AssetBundles.Editor
{
    [CustomEditor(typeof(AssetBundleBuildAsset), true)]
    internal class AssetBundleBuildAssetEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyScript;
        private SerializedProperty m_propertyOutputPath;
        private SerializedProperty m_propertyOptions;
        private SerializedProperty m_propertyUpdateCrc;
        private SerializedProperty m_propertyUpdateDependencies;
        private AssetBundleBuildAssetListDrawer m_listAssetBundles;

        private void OnEnable()
        {
            m_propertyScript = serializedObject.FindProperty("m_Script");
            m_propertyOutputPath = serializedObject.FindProperty("m_outputPath");
            m_propertyOptions = serializedObject.FindProperty("m_options");
            m_propertyUpdateCrc = serializedObject.FindProperty("m_updateCrc");
            m_propertyUpdateDependencies = serializedObject.FindProperty("m_updateDependencies");
            m_listAssetBundles = new AssetBundleBuildAssetListDrawer(serializedObject.FindProperty("m_assetBundles"), m_propertyOutputPath);
            m_listAssetBundles.Drawer.DisplayTitlebar = true;

            m_listAssetBundles.Enable();
        }

        private void OnDisable()
        {
            m_listAssetBundles.Disable();
        }

        public override void OnInspectorGUI()
        {
            bool build = false;

            using (new SerializedObjectUpdateScope(serializedObject))
            {
                using (new EditorGUI.DisabledScope(true))
                {
                    EditorGUILayout.PropertyField(m_propertyScript);
                }

                EditorGUILayout.PropertyField(m_propertyOutputPath);
                EditorGUILayout.PropertyField(m_propertyOptions);
                EditorGUILayout.PropertyField(m_propertyUpdateCrc);
                EditorGUILayout.PropertyField(m_propertyUpdateDependencies);

                m_listAssetBundles.DrawGUILayout();

                EditorGUILayout.Space();

                using (new EditorGUILayout.HorizontalScope())
                {
                    GUILayout.FlexibleSpace();

                    using (new EditorGUI.DisabledScope(!Directory.Exists(m_propertyOutputPath.stringValue)))
                    {
                        if (GUILayout.Button("Clear", GUILayout.Width(75F)))
                        {
                            OnClear();
                        }
                    }

                    using (new EditorGUI.DisabledScope(EditorApplication.isPlayingOrWillChangePlaymode))
                    {
                        if (GUILayout.Button("Build", GUILayout.Width(75F)))
                        {
                            build = true;
                        }
                    }
                }
            }

            EditorGUILayout.Space();
            EditorGUILayout.HelpBox("This is Editor Only asset.", MessageType.Info);
            EditorGUILayout.Space();

            if (m_listAssetBundles.Drawer.HasData)
            {
                m_listAssetBundles.DrawSelectedLayout();
            }
            else
            {
                if (EditorApplication.isPlayingOrWillChangePlaymode)
                {
                    EditorGUILayout.HelpBox("Previewing Asset Bundle unavailable in play mode.", MessageType.Info);
                }
                else
                {
                    if (m_listAssetBundles.HasSelection)
                    {
                        EditorGUILayout.HelpBox("Selected Asset Bundle file not found, build required.", MessageType.Info);
                    }
                    else
                    {
                        EditorGUILayout.HelpBox("Select any Asset Bundle to display.", MessageType.Info);
                    }
                }
            }

            if (build)
            {
                OnBuild();
            }
        }

        private void OnBuild()
        {
            m_listAssetBundles.ClearSelection();

            var asset = (AssetBundleBuildAsset)target;

            AssetBundleBuildEditorUtility.Build(asset);
            AssetDatabase.SaveAssets();
            Selection.activeObject = target;
        }

        private void OnClear()
        {
            m_listAssetBundles.ClearSelection();

            var asset = (AssetBundleBuildAsset)target;

            AssetDatabase.DeleteAsset(asset.OutputPath);
        }
    }
}
