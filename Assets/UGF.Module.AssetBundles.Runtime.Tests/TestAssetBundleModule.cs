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

            var asset = assetModule.Load<Material>("8465382ca3b5f744aa10a0f5054cf171", AssetBundleAssetLoadParameters.Default);

            Assert.NotNull(asset);
            Assert.AreEqual("Material_2", asset.name);

            assetModule.Unload("8465382ca3b5f744aa10a0f5054cf171", asset, AssetBundleAssetUnloadParameters.Default);

            Assert.True(asset == null);

            application.Uninitialize();
        }
    }
}
