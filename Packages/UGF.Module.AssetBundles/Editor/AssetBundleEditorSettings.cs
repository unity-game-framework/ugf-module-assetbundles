﻿using UGF.CustomSettings.Editor;
using UnityEditor;

namespace UGF.Module.AssetBundles.Editor
{
    [InitializeOnLoad]
    public static class AssetBundleEditorSettings
    {
        public static CustomSettingsEditorPackage<AssetBundleEditorSettingsData> Settings { get; } = new CustomSettingsEditorPackage<AssetBundleEditorSettingsData>
        (
            "UGF.Module.AssetBundles",
            "AssetBundleEditorSettings"
        );

        static AssetBundleEditorSettings()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        public static void BuildAll()
        {
            AssetBundleEditorSettingsData data = Settings.GetData();

            AssetBundleBuildEditorUtility.BuildAll(data.Builds);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        public static void ClearAll()
        {
            AssetBundleEditorSettingsData data = Settings.GetData();

            AssetBundleBuildEditorUtility.ClearAll(data.Builds);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        [SettingsProvider]
        private static SettingsProvider GetProvider()
        {
            return new CustomSettingsProvider<AssetBundleEditorSettingsData>("Project/Unity Game Framework/Asset Bundles", Settings, SettingsScope.Project);
        }

        private static void OnPlayModeStateChanged(PlayModeStateChange change)
        {
            if (change == PlayModeStateChange.ExitingEditMode)
            {
                AssetBundleEditorSettingsData data = Settings.GetData();

                if (data.BuildBeforeEnterPlayMode)
                {
                    BuildAll();
                }
            }
        }
    }
}
