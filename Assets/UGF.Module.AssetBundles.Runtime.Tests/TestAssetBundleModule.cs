using System.Collections.Generic;
using NUnit.Framework;
using UGF.Application.Runtime;
using UGF.Builder.Runtime;
using UGF.EditorTools.Runtime.Ids;
using UGF.Module.Assets.Runtime;
using UnityEngine;

namespace UGF.Module.AssetBundles.Runtime.Tests
{
    public class TestAssetBundleModule
    {
        [Test]
        public void InitializeAndUninitialize()
        {
            var application = new Application.Runtime.Application(new ApplicationDescription(false, new Dictionary<GlobalId, IBuilder<IApplication, IApplicationModule>>
            {
                { new GlobalId("4614ceca8914e5b4d8326f86aded3229"), Resources.Load<ApplicationModuleAsset>("AssetModule") },
                { new GlobalId("62bfbda34f5140deae6b6007d172c728"), Resources.Load<ApplicationModuleAsset>("AssetBundleModule") },
            }));

            application.Initialize();

            var module = application.GetModule<AssetBundleModule>();

            Assert.NotNull(module);

            application.Uninitialize();
        }

        [Test]
        public void LoadAndUnloadAsset()
        {
            var application = new Application.Runtime.Application(new ApplicationDescription(false, new Dictionary<GlobalId, IBuilder<IApplication, IApplicationModule>>
            {
                { new GlobalId("4614ceca8914e5b4d8326f86aded3229"), Resources.Load<ApplicationModuleAsset>("AssetModule") },
                { new GlobalId("62bfbda34f5140deae6b6007d172c728"), Resources.Load<ApplicationModuleAsset>("AssetBundleModule") },
            }));

            application.Initialize();

            var assetModule = application.GetModule<IAssetModule>();

            var asset = assetModule.Load<Material>(new GlobalId("8465382ca3b5f744aa10a0f5054cf171"));
            var asset2 = assetModule.Load<Sprite>(new GlobalId("961c71ba6ddebb4439671d6e489d80d6"));

            Assert.NotNull(asset);
            Assert.NotNull(asset2);
            Assert.AreEqual("Material_2", asset.name);
            Assert.IsInstanceOf<Sprite>(asset2);

            assetModule.Unload(new GlobalId("8465382ca3b5f744aa10a0f5054cf171"), asset);
            assetModule.Unload(new GlobalId("961c71ba6ddebb4439671d6e489d80d6"), asset2);

            Assert.True(asset == null);
            Assert.True(asset2 == null);

            application.Uninitialize();
        }
    }
}
