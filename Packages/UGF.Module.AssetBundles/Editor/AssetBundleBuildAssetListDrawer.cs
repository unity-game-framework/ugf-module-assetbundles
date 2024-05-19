using System;
using System.IO;
using UGF.AssetBundles.Editor;
using UGF.EditorTools.Editor.IMGUI;
using UnityEditor;
using Object = UnityEngine.Object;

namespace UGF.Module.AssetBundles.Editor
{
    internal class AssetBundleBuildAssetListDrawer : ReorderableListDrawer
    {
        public SerializedProperty PropertyOutputPath { get; }
        public AssetBundleFileDrawer FileDrawer { get; } = new AssetBundleFileDrawer();
        public EditorDrawer AssetDrawer { get; } = new EditorDrawer();
        public bool HasSelection { get { return List.selectedIndices.Count > 0; } }

        public AssetBundleBuildAssetListDrawer(SerializedProperty serializedProperty, SerializedProperty propertyOutputPath) : base(serializedProperty)
        {
            PropertyOutputPath = propertyOutputPath ?? throw new ArgumentNullException(nameof(propertyOutputPath));
            FileDrawer.DisplayMenuClear = false;
            AssetDrawer.DisplayTitlebar = true;
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            FileDrawer.Enable();
            AssetDrawer.Enable();
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            ClearSelection();

            FileDrawer.Disable();
            AssetDrawer.Disable();
        }

        protected override void OnRemove()
        {
            base.OnRemove();

            UpdateSelection();
        }

        protected override void OnSelect()
        {
            base.OnSelect();

            UpdateSelection();
        }

        public void DrawSelectedLayout()
        {
            if (AssetDrawer.HasEditor)
            {
                AssetDrawer.DrawGUILayout();
            }

            if (FileDrawer.HasData)
            {
                FileDrawer.DrawGUILayout();
            }
        }

        public void ClearSelection()
        {
            FileDrawer.Clear();
            AssetDrawer.Clear();
        }

        private void UpdateSelection()
        {
            ClearSelection();

            if (!EditorApplication.isPlayingOrWillChangePlaymode && List.index >= 0 && List.index < List.count)
            {
                SerializedProperty propertyElement = SerializedProperty.GetArrayElementAtIndex(List.index);
                Object element = propertyElement.objectReferenceValue;

                if (element != null)
                {
                    AssetDrawer.Set(element);

                    string guid = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(element));
                    string path = Path.Combine(PropertyOutputPath.stringValue, guid);

                    if (File.Exists(path))
                    {
                        FileDrawer.Set(path);
                    }
                }
            }
        }
    }
}
