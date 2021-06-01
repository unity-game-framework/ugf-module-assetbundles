using NUnit.Framework;
using UGF.Application.Runtime;
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
    }
}
