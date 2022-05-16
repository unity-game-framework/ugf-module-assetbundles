using NUnit.Framework;
using UGF.Application.Runtime;
using UGF.Module.Assets.Runtime;
using UnityEngine;

namespace UGF.Module.AssetBundles.Runtime.Tests
{
    public class TestAssetBundleModule
    {
        [Test]
        public void InitializeAndUninitialize()
        {
            var application = new ApplicationConfigured(new ApplicationResources
            {
                new ApplicationConfig
                {
                    Modules =
                    {
                        (IApplicationModuleBuilder)Resources.Load("AssetModule", typeof(IApplicationModuleBuilder)),
                        (IApplicationModuleBuilder)Resources.Load("AssetBundleModule", typeof(IApplicationModuleBuilder))
                    }
                }
            });

            application.Initialize();

            var module = application.GetModule<AssetBundleModule>();

            Assert.NotNull(module);

            application.Uninitialize();
        }

        [Test]
        public void LoadAndUnloadAsset()
        {
            var application = new ApplicationConfigured(new ApplicationResources
            {
                new ApplicationConfig
                {
                    Modules =
                    {
                        (IApplicationModuleBuilder)Resources.Load("AssetModule", typeof(IApplicationModuleBuilder)),
                        (IApplicationModuleBuilder)Resources.Load("AssetBundleModule", typeof(IApplicationModuleBuilder))
                    }
                }
            });

            application.Initialize();

            var assetModule = application.GetModule<IAssetModule>();

            var asset = assetModule.Load<Material>("8465382ca3b5f744aa10a0f5054cf171");
            var asset2 = assetModule.Load<Sprite>("961c71ba6ddebb4439671d6e489d80d6");

            Assert.NotNull(asset);
            Assert.NotNull(asset2);
            Assert.AreEqual("Material_2", asset.name);
            Assert.IsInstanceOf<Sprite>(asset2);

            assetModule.Unload("8465382ca3b5f744aa10a0f5054cf171", asset);
            assetModule.Unload("961c71ba6ddebb4439671d6e489d80d6", asset2);

            Assert.True(asset == null);
            Assert.True(asset2 == null);

            application.Uninitialize();
        }
    }
}
