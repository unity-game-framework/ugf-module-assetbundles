using System;
using System.Collections.Generic;

namespace UGF.Module.AssetBundles.Editor
{
    public class AssetBundleEditorInfo
    {
        public string Name { get; }
        public uint Crc { get; }
        public IReadOnlyList<AssetInfo> Assets { get; }
        public IReadOnlyList<string> Dependencies { get; }
        public bool IsStreamedSceneAssetBundle { get; }

        public class AssetInfo
        {
            public string Name { get; }
            public Type Type { get; }

            public AssetInfo(string name, Type type)
            {
                if (string.IsNullOrEmpty(name)) throw new ArgumentException("Value cannot be null or empty.", nameof(name));

                Name = name;
                Type = type ?? throw new ArgumentNullException(nameof(type));
            }
        }

        public AssetBundleEditorInfo(string name, uint crc, IReadOnlyList<AssetInfo> assets, IReadOnlyList<string> dependencies, bool isStreamedSceneAssetBundle)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("Value cannot be null or empty.", nameof(name));

            Name = name;
            Crc = crc;
            Assets = assets ?? throw new ArgumentNullException(nameof(assets));
            Dependencies = dependencies ?? throw new ArgumentNullException(nameof(dependencies));
            IsStreamedSceneAssetBundle = isStreamedSceneAssetBundle;
        }
    }
}
