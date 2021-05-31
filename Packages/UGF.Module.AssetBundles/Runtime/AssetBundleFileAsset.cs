﻿using System.Collections.Generic;
using UGF.EditorTools.Runtime.IMGUI.Attributes;
using UGF.Module.Assets.Runtime;
using UnityEngine;

namespace UGF.Module.AssetBundles.Runtime
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Assets/Asset Bundle File", order = 2000)]
    public class AssetBundleFileAsset : AssetBundleAsset
    {
        [AssetGuid(typeof(AssetLoaderAsset))]
        [SerializeField] private string m_loader;
        [AssetGuid(typeof(AssetBundleStorageAsset))]
        [SerializeField] private string m_storage;
        [SerializeField] private uint m_crc;
        [SerializeField] private ulong m_offset;
        [AssetGuid(typeof(AssetBundleAsset))]
        [SerializeField] private List<string> m_dependencies = new List<string>();

        public string Loader { get { return m_loader; } set { m_loader = value; } }
        public string Storage { get { return m_storage; } set { m_storage = value; } }
        public uint Crc { get { return m_crc; } set { m_crc = value; } }
        public ulong Offset { get { return m_offset; } set { m_offset = value; } }
        public List<string> Dependencies { get { return m_dependencies; } }

        protected override AssetBundleInfo OnBuild()
        {
            var info = new AssetBundleFileInfo(m_loader, m_storage)
            {
                Crc = m_crc,
                Offset = m_offset
            };

            for (int i = 0; i < m_dependencies.Count; i++)
            {
                string dependency = m_dependencies[i];

                info.Dependencies.Add(dependency);
            }

            return info;
        }
    }
}
