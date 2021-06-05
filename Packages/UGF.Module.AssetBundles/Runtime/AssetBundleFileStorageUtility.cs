using System;
using System.IO;

namespace UGF.Module.AssetBundles.Runtime
{
    public static class AssetBundleFileStorageUtility
    {
        public static string GetPath(AssetBundleFileStorageDirectory directory, string path)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentException("Value cannot be null or empty.", nameof(path));

            switch (directory)
            {
                case AssetBundleFileStorageDirectory.None: return path;
                case AssetBundleFileStorageDirectory.Data: return Path.Combine(UnityEngine.Application.dataPath, path);
                case AssetBundleFileStorageDirectory.StreamingAssets: return Path.Combine(UnityEngine.Application.streamingAssetsPath, path);
                case AssetBundleFileStorageDirectory.PersistentData: return Path.Combine(UnityEngine.Application.persistentDataPath, path);
                case AssetBundleFileStorageDirectory.TemporaryCache: return Path.Combine(UnityEngine.Application.temporaryCachePath, path);
                default:
                    throw new ArgumentOutOfRangeException(nameof(directory), directory, null);
            }
        }
    }
}
