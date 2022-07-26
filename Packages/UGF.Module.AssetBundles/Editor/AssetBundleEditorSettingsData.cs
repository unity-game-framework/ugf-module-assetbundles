using System.Collections.Generic;
using UGF.CustomSettings.Runtime;
using UnityEngine;

namespace UGF.Module.AssetBundles.Editor
{
    public class AssetBundleEditorSettingsData : CustomSettingsData
    {
        [SerializeField] private bool m_buildBeforeEnterPlaymode;
        [SerializeField] private List<AssetBundleBuildAsset> m_builds = new List<AssetBundleBuildAsset>();

        public bool BuildBeforeEnterPlaymode { get { return m_buildBeforeEnterPlaymode; } set { m_buildBeforeEnterPlaymode = value; } }
        public List<AssetBundleBuildAsset> Builds { get { return m_builds; } }
    }
}
