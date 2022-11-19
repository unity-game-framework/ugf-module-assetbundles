using System.Collections.Generic;
using UGF.Build.Editor;
using UnityEngine;

namespace UGF.Module.AssetBundles.Editor.Build
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Assets/Asset Bundle Step", order = 2000)]
    public class AssetBundleStepAsset : BuildStepAsset
    {
        [SerializeField] private List<AssetBundleBuildAsset> m_builds = new List<AssetBundleBuildAsset>();

        public List<AssetBundleBuildAsset> Builds { get { return m_builds; } }

        protected override IBuildStep OnBuild()
        {
            return new AssetBundleStep(m_builds);
        }
    }
}
