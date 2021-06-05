using System;
using System.Collections.Generic;
using UnityEngine;

namespace UGF.Module.AssetBundles.Editor
{
    internal class AssetBundleEditorInfoContainer : ScriptableObject
    {
        [SerializeField] private string m_path;
        [SerializeField] private string m_name;
        [SerializeField] private uint m_crc;
        [SerializeField] private bool m_isStreamedSceneAssetBundle;
        [SerializeField] private long m_size;
        [SerializeField] private List<AssetInfo> m_assets = new List<AssetInfo>();
        [SerializeField] private List<string> m_dependencies = new List<string>();

        public string Path { get { return m_path; } set { m_path = value; } }
        public string Name { get { return m_name; } set { m_name = value; } }
        public uint Crc { get { return m_crc; } set { m_crc = value; } }
        public bool IsStreamedSceneAssetBundle { get { return m_isStreamedSceneAssetBundle; } set { m_isStreamedSceneAssetBundle = value; } }
        public long Size { get { return m_size; } set { m_size = value; } }
        public List<AssetInfo> Assets { get { return m_assets; } }
        public List<string> Dependencies { get { return m_dependencies; } }

        [Serializable]
        public class AssetInfo
        {
            [SerializeField] private string m_name;
            [SerializeField] private string m_type;
            [SerializeField] private string m_address;
            [SerializeField] private long m_size;

            public string Name { get { return m_name; } set { m_name = value; } }
            public string Type { get { return m_type; } set { m_type = value; } }
            public string Address { get { return m_address; } set { m_address = value; } }
            public long Size { get { return m_size; } set { m_size = value; } }
        }
    }
}
