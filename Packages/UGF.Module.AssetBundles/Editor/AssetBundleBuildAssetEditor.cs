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
        private AssetBundleAssetListDrawer m_listAssetBundles;

        private void OnEnable()
        {
            m_propertyScript = serializedObject.FindProperty("m_Script");
            m_propertyOutputPath = serializedObject.FindProperty("m_outputPath");
            m_propertyOptions = serializedObject.FindProperty("m_options");
            m_propertyUpdateCrc = serializedObject.FindProperty("m_updateCrc");
            m_propertyUpdateDependencies = serializedObject.FindProperty("m_updateDependencies");
            m_listAssetBundles = new AssetBundleAssetListDrawer(serializedObject.FindProperty("m_assetBundles"), m_propertyOutputPath.stringValue);
            m_listAssetBundles.Drawer.DisplayTitlebar = true;

            m_listAssetBundles.Enable();
        }

        private void OnDisable()
        {
            m_listAssetBundles.Disable();
        }

        public override void OnInspectorGUI()
        {
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

                    if (GUILayout.Button("Build", GUILayout.Width(75F)))
                    {
                        OnBuild();
                    }
                }
            }

            EditorGUILayout.Space();
            EditorGUILayout.HelpBox("This is Editor Only asset.", MessageType.Info);
            EditorGUILayout.Space();

            if (m_listAssetBundles.Drawer.HasEditor)
            {
                m_listAssetBundles.DrawSelectedLayout();
            }
            else
            {
                EditorGUILayout.HelpBox("Select any Asset Bundle to display.", MessageType.Info);
            }
        }

        private void OnBuild()
        {
            var asset = (AssetBundleBuildAsset)target;

            AssetBundleBuildEditorUtility.Build(asset);
            AssetDatabase.SaveAssets();
        }
    }
}
