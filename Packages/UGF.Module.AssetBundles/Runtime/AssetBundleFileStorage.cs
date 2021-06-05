using System;
using System.IO;
using UGF.Module.Assets.Runtime;
using UGF.RuntimeTools.Runtime.Contexts;

namespace UGF.Module.AssetBundles.Runtime
{
    public class AssetBundleFileStorage : AssetBundleStorage<AssetBundleFileInfo>
    {
        public string DirectoryPath { get; }

        public AssetBundleFileStorage(string directoryPath)
        {
            if (string.IsNullOrEmpty(directoryPath)) throw new ArgumentException("Value cannot be null or empty.", nameof(directoryPath));

            DirectoryPath = directoryPath;
        }

        protected override string OnGetAddress(AssetBundleFileInfo info, string id, Type type, IAssetLoadParameters parameters, IContext context)
        {
            string path = DirectoryPath;

            path = Path.Combine(path, id);

            return path;
        }
    }
}
