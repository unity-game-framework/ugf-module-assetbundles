using System;
using System.IO;
using UGF.EditorTools.Editor.IMGUI;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UGF.Module.AssetBundles.Editor
{
    internal class AssetBundleBuildAssetListDrawer : ReorderableListDrawer
    {
        public SerializedProperty PropertyOutputPath { get; }
        public EditorDrawer Drawer { get; } = new EditorDrawer();
        public bool HasSelection { get { return List.selectedIndices.Count > 0; } }

        public AssetBundleBuildAssetListDrawer(SerializedProperty serializedProperty, SerializedProperty propertyOutputPath) : base(serializedProperty)
        {
            PropertyOutputPath = propertyOutputPath ?? throw new ArgumentNullException(nameof(propertyOutputPath));
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
            if (Drawer.HasEditor)
            {
                Object target = Drawer.Editor.target;

                Drawer.Clear();

                Object.DestroyImmediate(target);
            }
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
                        AssetBundleEditorInfo info = AssetBundleEditorUtility.LoadInfo(path);
                        AssetBundleEditorInfoContainer container = AssetBundleEditorInfoContainerUtility.CreateContainer(info);

                        container.Path = path;
                        container.name = element.name;
                        container.hideFlags = HideFlags.NotEditable;

                        Drawer.Set(container);
                    }
                }
            }
        }
    }
}
