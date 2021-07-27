using System;
using UnityEngine;

namespace UGF.Module.AssetBundles.Editor
{
    internal static class AssetBundleEditorInfoContainerUtility
    {
        public static bool DebugDisplay { get; set; }

        public static AssetBundleEditorInfoContainer CreateContainer(AssetBundleEditorInfo info)
        {
            if (info == null) throw new ArgumentNullException(nameof(info));

            var container = ScriptableObject.CreateInstance<AssetBundleEditorInfoContainer>();

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

            return container;
        }
    }
}
