using System.Collections.Generic;
using UGF.EditorTools.Runtime.IMGUI.AssetReferences;
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
        [SerializeField] private List<AssetReference<AssetBundleAsset>> m_assetBundles = new List<AssetReference<AssetBundleAsset>>();

        public string OutputPath { get { return m_outputPath; } set { m_outputPath = value; } }
        public BuildAssetBundleOptions Options { get { return m_options; } set { m_options = value; } }
        public List<AssetReference<AssetBundleAsset>> AssetBundles { get { return m_assetBundles; } }
    }
}
