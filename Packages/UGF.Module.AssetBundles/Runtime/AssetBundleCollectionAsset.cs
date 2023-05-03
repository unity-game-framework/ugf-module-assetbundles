using System;
using System.Collections.Generic;
using UGF.Builder.Runtime;
using UGF.EditorTools.Runtime.Ids;
using UnityEngine;

namespace UGF.Module.AssetBundles.Runtime
{
    public abstract class AssetBundleCollectionAsset : ScriptableObject
    {
        public void GetAssetBundles(IDictionary<GlobalId, IBuilder<IAssetBundleInfo>> assetBundles)
        {
            if (assetBundles == null) throw new ArgumentNullException(nameof(assetBundles));

            OnGetAssetBundles(assetBundles);
        }

        protected abstract void OnGetAssetBundles(IDictionary<GlobalId, IBuilder<IAssetBundleInfo>> assetBundles);
    }
}
