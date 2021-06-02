using UGF.EditorTools.Editor.IMGUI.AssetReferences;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;

namespace UGF.Module.AssetBundles.Editor
{
    [CustomEditor(typeof(AssetBundleBuildAsset), true)]
    internal class AssetBundleBuildAssetEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyScript;
        private SerializedProperty m_propertyOutputPath;
        private SerializedProperty m_propertyOptions;
        private AssetReferenceListDrawer m_listAssetBundles;

        private void OnEnable()
        {
            m_propertyScript = serializedObject.FindProperty("m_Script");
            m_propertyOutputPath = serializedObject.FindProperty("m_outputPath");
            m_propertyOptions = serializedObject.FindProperty("m_options");
            m_listAssetBundles = new AssetReferenceListDrawer(serializedObject.FindProperty("m_assetBundles"));

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

                m_listAssetBundles.DrawGUILayout();
            }
        }
    }
}
