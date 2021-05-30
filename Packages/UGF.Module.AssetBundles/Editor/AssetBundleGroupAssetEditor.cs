using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UGF.Module.AssetBundles.Runtime;
using UnityEditor;

namespace UGF.Module.AssetBundles.Editor
{
    [CustomEditor(typeof(AssetBundleGroupAsset), true)]
    internal class AssetBundleGroupAssetEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyScript;
        private SerializedProperty m_propertyLoader;
        private SerializedProperty m_propertyAssetBundle;
        private ReorderableListDrawer m_listAssets;

        private void OnEnable()
        {
            m_propertyScript = serializedObject.FindProperty("m_Script");
            m_propertyLoader = serializedObject.FindProperty("m_loader");
            m_propertyAssetBundle = serializedObject.FindProperty("m_assetBundle");
            m_listAssets = new ReorderableListDrawer(serializedObject.FindProperty("m_assets"));

            m_listAssets.Enable();
        }

        private void OnDisable()
        {
            m_listAssets.Disable();
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
                EditorGUILayout.PropertyField(m_propertyAssetBundle);

                m_listAssets.DrawGUILayout();
            }
        }
    }
}
