using System.IO;
using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;
using UnityEngine;

namespace UGF.Module.AssetBundles.Editor
{
    [CustomEditor(typeof(AssetBundleBuildAsset), true)]
    internal class AssetBundleBuildAssetEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyOutputPath;
        private SerializedProperty m_propertyOptions;
        private SerializedProperty m_propertyIncludeDependencies;
        private SerializedProperty m_propertyUpdateCrc;
        private SerializedProperty m_propertyUpdateDependencies;
        private SerializedProperty m_propertyClearManifests;
        private AssetBundleBuildAssetListDrawer m_listAssetBundles;
        private Styles m_styles;

        private class Styles
        {
            public GUIContent IncludeDependenciesContent { get; } = new GUIContent("Include Dependencies", "Enabling this parameter will collect dependencies for each asset in asset bundle " +
                                                                                                           "and make them to be explicitly included with asset guid as address. " +
                                                                                                           "If the next asset bundle would have any asset with dependencies already included in previous asset bundle, " +
                                                                                                           "they will be ignored and asset bundle will have previous one as dependency.");

            public GUIContent UpdateCrc { get; } = new GUIContent("Update Crc", "Determines whether to update 'Crc' property value of the AssetBundleAsset assets.");
            public GUIContent UpdateDependencies { get; } = new GUIContent("Update Dependencies", "Determines whether to update 'Dependencies' list of the AssetBundleAsset assets.");
        }

        private void OnEnable()
        {
            m_propertyOutputPath = serializedObject.FindProperty("m_outputPath");
            m_propertyOptions = serializedObject.FindProperty("m_options");
            m_propertyIncludeDependencies = serializedObject.FindProperty("m_includeDependencies");
            m_propertyUpdateCrc = serializedObject.FindProperty("m_updateCrc");
            m_propertyUpdateDependencies = serializedObject.FindProperty("m_updateDependencies");
            m_propertyClearManifests = serializedObject.FindProperty("m_clearManifests");

            m_listAssetBundles = new AssetBundleBuildAssetListDrawer(serializedObject.FindProperty("m_assetBundles"), m_propertyOutputPath)
            {
                FileDrawer = { DisplayTitlebar = true }
            };

            m_listAssetBundles.Enable();
        }

        private void OnDisable()
        {
            m_listAssetBundles.Disable();
        }

        public override void OnInspectorGUI()
        {
            m_styles ??= new Styles();

            using (new SerializedObjectUpdateScope(serializedObject))
            {
                EditorIMGUIUtility.DrawScriptProperty(serializedObject);

                EditorGUILayout.PropertyField(m_propertyOutputPath);
                EditorGUILayout.PropertyField(m_propertyOptions);
                EditorGUILayout.PropertyField(m_propertyIncludeDependencies, m_styles.IncludeDependenciesContent);
                EditorGUILayout.PropertyField(m_propertyUpdateCrc, m_styles.UpdateCrc);
                EditorGUILayout.PropertyField(m_propertyUpdateDependencies, m_styles.UpdateDependencies);
                EditorGUILayout.PropertyField(m_propertyClearManifests);

                m_listAssetBundles.DrawGUILayout();
            }

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
                        OnBuild();
                    }
                }
            }

            EditorGUILayout.Space();
            EditorGUILayout.HelpBox("This is Editor Only asset.", MessageType.Info);
            EditorGUILayout.Space();

            m_listAssetBundles.DrawSelectedLayout();

            if (!m_listAssetBundles.FileDrawer.HasData)
            {
                if (EditorApplication.isPlayingOrWillChangePlaymode)
                {
                    EditorGUILayout.HelpBox("Previewing Asset Bundle unavailable in play mode.", MessageType.Info);
                }
                else
                {
                    EditorGUILayout.HelpBox(m_listAssetBundles.HasSelection ? "Selected Asset Bundle file not found, build required." : "Select any Asset Bundle to display.", MessageType.Info);
                }
            }
        }

        private void OnBuild()
        {
            m_listAssetBundles.ClearSelection();

            var asset = (AssetBundleBuildAsset)target;

            AssetBundleBuildEditorUtility.Build(asset);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Selection.activeObject = target;
            GUIUtility.ExitGUI();
        }

        private void OnClear()
        {
            m_listAssetBundles.ClearSelection();

            var asset = (AssetBundleBuildAsset)target;

            AssetBundleBuildEditorUtility.Clear(asset);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
