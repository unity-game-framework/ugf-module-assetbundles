using System.Collections.Generic;
using UGF.CustomSettings.Runtime;
using UnityEngine;

namespace UGF.Module.AssetBundles.Editor
{
    public class AssetBundleEditorSettingsData : CustomSettingsData
    {
        [SerializeField] private bool m_buildBeforeEnterPlayMode;
        [SerializeField] private List<AssetBundleBuildAsset> m_builds = new List<AssetBundleBuildAsset>();

        public bool BuildBeforeEnterPlayMode { get { return m_buildBeforeEnterPlayMode; } set { m_buildBeforeEnterPlayMode = value; } }
        public List<AssetBundleBuildAsset> Builds { get { return m_builds; } }
    }
}
