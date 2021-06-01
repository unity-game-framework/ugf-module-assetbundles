﻿using System.Collections.Generic;
using UGF.Builder.Runtime;
using UGF.EditorTools.Runtime.IMGUI.Attributes;
using UGF.Module.Assets.Runtime;
using UnityEngine;

namespace UGF.Module.AssetBundles.Runtime
{
    public abstract class AssetBundleAsset : BuilderAsset<IAssetBundleInfo>
    {
        [AssetGuid(typeof(AssetLoaderAsset))]
        [SerializeField] private string m_loader;
        [AssetGuid(typeof(AssetBundleAsset))]
        [SerializeField] private List<string> m_dependencies = new List<string>();

        public string Loader { get { return m_loader; } set { m_loader = value; } }
        public List<string> Dependencies { get { return m_dependencies; } }
    }
}
