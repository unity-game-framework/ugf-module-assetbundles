using UGF.EditorTools.Editor.IMGUI.PropertyDrawers;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;
using UnityEngine;

namespace UGF.Module.AssetBundles.Editor
{
    [CustomPropertyDrawer(typeof(AssetBundleEditorInfoContainer.AssetInfo))]
    internal class AssetBundleEditorInfoAssetInfoDrawer : PropertyDrawerBase
    {
        protected override void OnDrawProperty(Rect position, SerializedProperty serializedProperty, GUIContent label)
        {
            SerializedProperty propertyName = serializedProperty.FindPropertyRelative("m_name");
            SerializedProperty propertyType = serializedProperty.FindPropertyRelative("m_type");
            SerializedProperty propertyAddress = serializedProperty.FindPropertyRelative("m_address");
            SerializedProperty propertySize = serializedProperty.FindPropertyRelative("m_size");

            float space = EditorGUIUtility.standardVerticalSpacing;
            float height = EditorGUIUtility.singleLineHeight;

            var rectFoldout = new Rect(position.x, position.y, position.width, height);
            var rectName = new Rect(position.x, rectFoldout.yMax + space, position.width, height);
            var rectType = new Rect(position.x, rectName.yMax + space, position.width, height);
            var rectAddress = new Rect(position.x, rectType.yMax + space, position.width, height);
            var rectSize = new Rect(position.x, rectAddress.yMax + space, position.width, height);

            serializedProperty.isExpanded = EditorGUI.Foldout(rectFoldout, serializedProperty.isExpanded, serializedProperty.displayName, true);

            if (serializedProperty.isExpanded)
            {
                using (new IndentIncrementScope(1))
                {
                    EditorGUI.PropertyField(rectName, propertyName);
                    EditorGUI.PropertyField(rectType, propertyType);
                    EditorGUI.PropertyField(rectAddress, propertyAddress);
                    EditorGUI.TextField(rectSize, propertySize.displayName, EditorUtility.FormatBytes(propertySize.longValue));
                }
            }
        }

        public override float GetPropertyHeight(SerializedProperty serializedProperty, GUIContent label)
        {
            float space = EditorGUIUtility.standardVerticalSpacing;
            float height = EditorGUIUtility.singleLineHeight;

            return serializedProperty.isExpanded ? height * 5 + space * 4 : height;
        }
    }
}
