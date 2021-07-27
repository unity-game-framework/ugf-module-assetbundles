using UnityEditor;

namespace UGF.Module.AssetBundles.Editor
{
    internal static class AssetBundleEditorInfoContainerEditorMenuContext
    {
        [MenuItem("CONTEXT/AssetBundleEditorInfoContainer/Debug", false, 2000)]
        private static void DebugMenu()
        {
            AssetBundleEditorInfoContainerUtility.DebugDisplay = !AssetBundleEditorInfoContainerUtility.DebugDisplay;

            Menu.SetChecked("CONTEXT/AssetBundleEditorInfoContainer/Debug", AssetBundleEditorInfoContainerUtility.DebugDisplay);
        }

        [MenuItem("CONTEXT/AssetBundleEditorInfoContainer/Debug", true, 2000)]
        private static bool DebugValidate()
        {
            Menu.SetChecked("CONTEXT/AssetBundleEditorInfoContainer/Debug", AssetBundleEditorInfoContainerUtility.DebugDisplay);

            return true;
        }
    }
}
