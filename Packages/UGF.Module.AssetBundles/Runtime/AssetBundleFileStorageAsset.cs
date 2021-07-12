using UGF.RuntimeTools.Runtime.Storage;
using UnityEngine;

namespace UGF.Module.AssetBundles.Runtime
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Assets/Asset Bundle File Storage", order = 2000)]
    public class AssetBundleFileStorageAsset : AssetBundleStorageAsset
    {
        [SerializeField] private StoragePathType m_directory;
        [SerializeField] private string m_directoryPath;

        public StoragePathType Directory { get { return m_directory; } set { m_directory = value; } }
        public string DirectoryPath { get { return m_directoryPath; } set { m_directoryPath = value; } }

        protected override IAssetBundleStorage OnBuild()
        {
            string path = StorageUtility.GetPath(m_directory, m_directoryPath);
            var storage = new AssetBundleFileStorage(path);

            return storage;
        }
    }
}
