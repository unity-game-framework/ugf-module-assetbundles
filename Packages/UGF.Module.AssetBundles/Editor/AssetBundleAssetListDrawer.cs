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
        public SerializedProperty PropertyOutputPath { get; }
        public EditorDrawer Drawer { get; } = new EditorDrawer();

        public AssetBundleAssetListDrawer(SerializedProperty serializedProperty, SerializedProperty propertyOutputPath) : base(serializedProperty)
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
                        var container = ScriptableObject.CreateInstance<AssetBundleEditorInfoContainer>();

                        container.Path = path;
                        container.name = element.name;
                        container.Name = info.Name;
                        container.Crc = info.Crc;
                        container.IsStreamedSceneAssetBundle = info.IsStreamedSceneAssetBundle;
                        container.Size = info.Size;
                        container.Dependencies.AddRange(info.Dependencies);

                        for (int i = 0; i < info.Assets.Count; i++)
                        {
                            AssetBundleEditorInfo.AssetInfo assetInfo = info.Assets[i];

                            container.Assets.Add(new AssetBundleEditorInfoContainer.AssetInfo
                            {
                                Name = assetInfo.Name,
                                Type = assetInfo.Type.FullName,
                                Address = assetInfo.Address,
                                Size = assetInfo.Size
                            });
                        }

                        Drawer.Set(container);
                    }
                    else
                    {
                        var container = ScriptableObject.CreateInstance<AssetBundleEditorInfoContainer>();

                        container.name = element.name;

                        Drawer.Set(container);
                    }
                }
            }
        }
    }
}
