using UnityEditor;

namespace UGF.Module.AssetBundles.Editor
{
    internal static class AssetBundleEditorInfoContainerEditorMenuContext
    {
        [MenuItem("CONTEXT/AssetBundleEditorInfoContainer/Debug", false, 2000)]
        private static void DebugMenu(MenuCommand menuCommand)
        {
            var context = (AssetBundleEditorInfoContainer)menuCommand.context;

            context.Debug = !context.Debug;

            Menu.SetChecked("CONTEXT/AssetBundleEditorInfoContainer/Debug", context.Debug);
        }

        [MenuItem("CONTEXT/AssetBundleEditorInfoContainer/Debug", true, 2000)]
        private static bool DebugValidate(MenuCommand menuCommand)
        {
            var context = (AssetBundleEditorInfoContainer)menuCommand.context;

            Menu.SetChecked("CONTEXT/AssetBundleEditorInfoContainer/Debug", context.Debug);

            return true;
        }
    }
}
