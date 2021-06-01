using System;
using System.IO;
using UGF.Module.Assets.Runtime;
using UGF.RuntimeTools.Runtime.Contexts;

namespace UGF.Module.AssetBundles.Runtime
{
    public class AssetBundleFileStorage : AssetBundleStorage<AssetBundleFileInfo>
    {
        public string RelativePath { get; }

        public AssetBundleFileStorage(string relativePath)
        {
            if (string.IsNullOrEmpty(relativePath)) throw new ArgumentException("Value cannot be null or empty.", nameof(relativePath));

            RelativePath = relativePath;
        }

        protected override string OnGetAddress(AssetBundleFileInfo info, string id, Type type, IAssetLoadParameters parameters, IContext context)
        {
            string root = UnityEngine.Application.streamingAssetsPath;
            string path = Path.Combine(root, RelativePath);

            path = Path.Combine(path, id);

            return path;
        }
    }
}
