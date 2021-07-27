using System;
using System.IO;
using UnityEngine;

namespace UGF.Module.AssetBundles.Editor
{
    public static class AssetBundleEditorInfoContainerUtility
    {
        public static bool DebugDisplay { get; set; }

        public static AssetBundleEditorInfoContainer CreateContainer(string path)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentException("Value cannot be null or empty.", nameof(path));

            AssetBundleEditorInfo info = AssetBundleEditorUtility.LoadInfo(path);
            AssetBundleEditorInfoContainer container = CreateContainer(info);

            container.Path = path;
            container.name = Path.GetFileNameWithoutExtension(path);
            container.hideFlags = HideFlags.NotEditable;

            return container;
        }

        public static AssetBundleEditorInfoContainer CreateContainer(AssetBundleEditorInfo info)
        {
            if (info == null) throw new ArgumentNullException(nameof(info));

            var container = ScriptableObject.CreateInstance<AssetBundleEditorInfoContainer>();

            container.Name = info.Name;
            container.Crc = info.Crc;
            container.IsStreamedSceneAssetBundle = info.IsStreamedSceneAssetBundle;
            container.Dependencies.AddRange(info.Dependencies);

            for (int i = 0; i < info.Assets.Count; i++)
            {
                AssetBundleEditorInfo.AssetInfo assetInfo = info.Assets[i];

                container.Assets.Add(new AssetBundleEditorInfoContainer.AssetInfo
                {
                    Name = assetInfo.Name,
                    Type = assetInfo.Type.FullName,
                    Address = assetInfo.Address
                });
            }

            return container;
        }
    }
}
