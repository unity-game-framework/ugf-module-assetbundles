using System.Collections.Generic;
using UGF.EditorTools.Runtime.IMGUI.AssetReferences;
using UGF.Module.AssetBundles.Runtime;
using UnityEngine;

namespace UGF.Module.AssetBundles.Editor
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Assets/Asset Bundle Build", order = 2000)]
    public class AssetBundleBuildAsset : ScriptableObject
    {
        [SerializeField] private List<AssetReference<AssetBundleAsset>> m_assetBundles = new List<AssetReference<AssetBundleAsset>>();

        public List<AssetReference<AssetBundleAsset>> AssetBundles { get { return m_assetBundles; } }
    }
}
