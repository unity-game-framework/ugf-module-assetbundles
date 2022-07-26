using UGF.CustomSettings.Editor;
using UnityEditor;

namespace UGF.Module.AssetBundles.Editor
{
    public static class AssetBundleEditorSettings
    {
        public static CustomSettingsEditorPackage<AssetBundleEditorSettingsData> Settings { get; } = new CustomSettingsEditorPackage<AssetBundleEditorSettingsData>
        (
            "UGF.Module.AssetBundles",
            "AssetBundleEditorSettings"
        );

        public static void BuildAll()
        {
            AssetBundleEditorSettingsData data = Settings.GetData();

            AssetBundleBuildEditorUtility.BuildAll(data.Builds);
        }

        public static void ClearAll()
        {
            AssetBundleEditorSettingsData data = Settings.GetData();

            AssetBundleBuildEditorUtility.ClearAll(data.Builds);
        }

        [SettingsProvider]
        private static SettingsProvider GetProvider()
        {
            return new CustomSettingsProvider<AssetBundleEditorSettingsData>("Project/Unity Game Framework/Asset Bundles", Settings, SettingsScope.Project);
        }

        [InitializeOnEnterPlayMode]
        private static void OnEnterPlayMode()
        {
            AssetBundleEditorSettingsData data = Settings.GetData();

            if (data.BuildBeforeEnterPlayMode)
            {
                BuildAll();
            }
        }
    }
}
