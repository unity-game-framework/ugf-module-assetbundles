using UnityEngine;

namespace UGF.Module.AssetBundles.Runtime.Tests
{
    [CreateAssetMenu(menuName = "Tests/TestAssetBundleAsset")]
    public class TestAssetBundleAsset : AssetBundleAsset
    {
        protected override IAssetBundleInfo OnBuild()
        {
            return null;
        }
    }
}
