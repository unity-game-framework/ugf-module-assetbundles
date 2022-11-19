using System;
using System.Collections.Generic;
using UGF.Build.Editor;
using UGF.Logs.Runtime;
using UGF.RuntimeTools.Runtime.Contexts;

namespace UGF.Module.AssetBundles.Editor.Build
{
    public class AssetBundleStep : BuildStep
    {
        public IReadOnlyList<AssetBundleBuildAsset> Builds { get; }

        public AssetBundleStep(IReadOnlyList<AssetBundleBuildAsset> builds)
        {
            Builds = builds ?? throw new ArgumentNullException(nameof(builds));
        }

        protected override void OnExecute(IBuildSetup setup, IContext context)
        {
            AssetBundleBuildEditorUtility.BuildAll(Builds);

            Log.Info("Asset bundles built", new { Builds.Count });
        }
    }
}
