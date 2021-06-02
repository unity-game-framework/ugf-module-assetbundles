using System;
using System.IO;
using UGF.EditorTools.Editor.IMGUI;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UGF.Module.AssetBundles.Editor
{
    internal class AssetBundleAssetListDrawer : ReorderableListDrawer
    {
        public string OutputPath { get; }
        public EditorDrawer Drawer { get; } = new EditorDrawer();

        public AssetBundleAssetListDrawer(SerializedProperty serializedProperty, string outputPath) : base(serializedProperty)
        {
            if (string.IsNullOrEmpty(outputPath)) throw new ArgumentException("Value cannot be null or empty.", nameof(outputPath));

            OutputPath = outputPath;
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            Drawer.Enable();
        }

        protected override void OnDisable()
        {
            base.OnDisable();

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

        private void UpdateSelection()
        {
            ClearSelection();

            if (List.index >= 0 && List.index < List.count)
            {
                SerializedProperty propertyElement = SerializedProperty.GetArrayElementAtIndex(List.index);
                Object element = propertyElement.objectReferenceValue;

                if (element != null)
                {
                    string guid = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(element));
                    string path = Path.Combine(OutputPath, guid);

                    if (File.Exists(path))
                    {
                        AssetBundleEditorInfo info = AssetBundleEditorUtility.LoadInfo(path);
                        var container = ScriptableObject.CreateInstance<AssetBundleEditorInfoContainer>();

                        container.name = element.name;
                        container.Name = info.Name;
                        container.Crc = info.Crc;
                        container.IsStreamedSceneAssetBundle = info.IsStreamedSceneAssetBundle;
                        container.AssetNames.AddRange(info.AssetNames);
                        container.Dependencies.AddRange(info.Dependencies);

                        Drawer.Set(container);
                    }
                }
            }
        }

        private void ClearSelection()
        {
            if (Drawer.HasEditor)
            {
                Object target = Drawer.Editor.target;

                Drawer.Clear();

                Object.DestroyImmediate(target);
            }
        }
    }
}
