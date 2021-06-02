using System.Collections.Generic;
using UGF.Module.AssetBundles.Runtime;
using UnityEditor;
using UnityEngine;

namespace UGF.Module.AssetBundles.Editor
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Assets/Asset Bundle Build", order = 2000)]
    public class AssetBundleBuildAsset : ScriptableObject
    {
        [SerializeField] private string m_outputPath;
        [SerializeField] private BuildAssetBundleOptions m_options = BuildAssetBundleOptions.None;
        [SerializeField] private bool m_updateCrc = true;
        [SerializeField] private bool m_updateDependencies = true;
        [SerializeField] private List<AssetBundleAsset> m_assetBundles = new List<AssetBundleAsset>();

        public string OutputPath { get { return m_outputPath; } set { m_outputPath = value; } }
        public BuildAssetBundleOptions Options { get { return m_options; } set { m_options = value; } }
        public bool UpdateCrc { get { return m_updateCrc; } set { m_updateCrc = value; } }
        public bool UpdateDependencies { get { return m_updateDependencies; } set { m_updateDependencies = value; } }
        public List<AssetBundleAsset> AssetBundles { get { return m_assetBundles; } }
    }
}
