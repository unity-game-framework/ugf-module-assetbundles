using System.Collections.Generic;
using UGF.EditorTools.Runtime.Assets;
using UGF.EditorTools.Runtime.Ids;
using UnityEngine;

namespace UGF.Module.AssetBundles.Runtime
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Assets/Asset Bundle Group Shared", order = 2000)]
    public class AssetBundleGroupSharedAsset : AssetBundleGroupAsset
    {
        [AssetId(typeof(AssetBundleGroupAsset))]
        [SerializeField] private List<GlobalId> m_groups = new List<GlobalId>();

        public List<GlobalId> Groups { get { return m_groups; } }
    }
}
