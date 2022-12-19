using System;
using UGF.EditorTools.Runtime.Ids;
using UGF.Module.Scenes.Runtime;

namespace UGF.Module.AssetBundles.Runtime.Scenes
{
    public class AssetBundleSceneInfo : SceneInfo
    {
        public GlobalId AssetBundleId { get; }

        public AssetBundleSceneInfo(GlobalId loaderId, GlobalId assetBundleId, string address) : base(loaderId, address)
        {
            if (!assetBundleId.IsValid()) throw new ArgumentException("Value should be valid.", nameof(assetBundleId));

            AssetBundleId = assetBundleId;
        }
    }
}
