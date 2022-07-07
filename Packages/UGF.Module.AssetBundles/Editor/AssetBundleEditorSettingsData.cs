using System.Collections.Generic;
using UGF.CustomSettings.Runtime;
using UnityEngine;

namespace UGF.Module.AssetBundles.Editor
{
    public class AssetBundleEditorSettingsData : CustomSettingsData
    {
        [SerializeField] private List<AssetBundleBuildAsset> m_builds = new List<AssetBundleBuildAsset>();

        public List<AssetBundleBuildAsset> Builds { get { return m_builds; } }
    }
}
