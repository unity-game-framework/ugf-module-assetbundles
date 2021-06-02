using System.Collections.Generic;
using UnityEngine;

namespace UGF.Module.AssetBundles.Editor
{
    internal class AssetBundleEditorInfoContainer : ScriptableObject
    {
        [SerializeField] private string m_name;
        [SerializeField] private uint m_crc;
        [SerializeField] private bool m_isStreamedSceneAssetBundle;
        [SerializeField] private List<string> m_assetNames = new List<string>();
        [SerializeField] private List<string> m_dependencies = new List<string>();

        public string Name { get { return m_name; } set { m_name = value; } }
        public uint Crc { get { return m_crc; } set { m_crc = value; } }
        public bool IsStreamedSceneAssetBundle { get { return m_isStreamedSceneAssetBundle; } set { m_isStreamedSceneAssetBundle = value; } }
        public List<string> AssetNames { get { return m_assetNames; } }
        public List<string> Dependencies { get { return m_dependencies; } }
    }
}
