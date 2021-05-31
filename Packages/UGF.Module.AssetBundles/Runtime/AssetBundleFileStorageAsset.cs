using UnityEngine;

namespace UGF.Module.AssetBundles.Runtime
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Assets/Asset Bundle File Storage", order = 2000)]
    public class AssetBundleFileStorageAsset : AssetBundleStorageAsset
    {
        [SerializeField] private string m_relativePath = "Bundles";

        public string RelativePath { get { return m_relativePath; } set { m_relativePath = value; } }

        protected override IAssetBundleStorage OnBuild()
        {
            var storage = new AssetBundleFileStorage(m_relativePath);

            return storage;
        }
    }
}
