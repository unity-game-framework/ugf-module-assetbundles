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
        public AssetBundleFileDrawer Drawer { get; } = new AssetBundleFileDrawer();
        public bool HasSelection { get { return List.selectedIndices.Count > 0; } }

        public AssetBundleBuildAssetListDrawer(SerializedProperty serializedProperty, SerializedProperty propertyOutputPath) : base(serializedProperty)
        {
            PropertyOutputPath = propertyOutputPath ?? throw new ArgumentNullException(nameof(propertyOutputPath));
            Drawer.DisplayMenuClear = false;
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            Drawer.Enable();
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            ClearSelection();

            Drawer.Disable();
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
            Drawer.DrawGUILayout();
        }

        public void ClearSelection()
        {
            Drawer.Clear();
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
                    string guid = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(element));
                    string path = Path.Combine(PropertyOutputPath.stringValue, guid);

                    if (File.Exists(path))
                    {
                        Drawer.Set(path);
                    }
                }
            }
        }
    }
}
