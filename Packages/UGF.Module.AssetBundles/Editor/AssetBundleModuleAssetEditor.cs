using UGF.EditorTools.Editor.IMGUI.AssetReferences;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UGF.Module.AssetBundles.Runtime;
using UnityEditor;

namespace UGF.Module.AssetBundles.Editor
{
    [CustomEditor(typeof(AssetBundleModuleAsset), true)]
    internal class AssetBundleModuleAssetEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyScript;
        private AssetReferenceListDrawer m_listAssetBundles;

        private void OnEnable()
        {
            m_propertyScript = serializedObject.FindProperty("m_Script");
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

                m_listAssetBundles.DrawGUILayout();
            }
        }
    }
}
