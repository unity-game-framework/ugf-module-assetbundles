using System;
using System.Collections.Generic;

namespace UGF.Module.AssetBundles.Editor
{
    public class AssetBundleEditorInfo
    {
        public string Name { get; }
        public uint Crc { get; }
        public IReadOnlyList<string> AssetNames { get; }
        public IReadOnlyList<string> Dependencies { get; }
        public bool IsStreamedSceneAssetBundle { get; }

        public AssetBundleEditorInfo(string name, uint crc, IReadOnlyList<string> assetNames, IReadOnlyList<string> dependencies, bool isStreamedSceneAssetBundle)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("Value cannot be null or empty.", nameof(name));

            Name = name;
            Crc = crc;
            AssetNames = assetNames ?? throw new ArgumentNullException(nameof(assetNames));
            Dependencies = dependencies ?? throw new ArgumentNullException(nameof(dependencies));
            IsStreamedSceneAssetBundle = isStreamedSceneAssetBundle;
        }
    }
}
