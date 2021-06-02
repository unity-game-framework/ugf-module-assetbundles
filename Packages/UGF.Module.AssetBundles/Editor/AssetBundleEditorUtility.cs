using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UGF.Module.AssetBundles.Editor
{
    public static class AssetBundleEditorUtility
    {
        public static AssetBundleManifest Build(IReadOnlyDictionary<string, IList<string>> groups, string outputPath, BuildAssetBundleOptions options = BuildAssetBundleOptions.None)
        {
            return Build(groups, outputPath, EditorUserBuildSettings.activeBuildTarget, options);
        }

        public static AssetBundleManifest Build(IReadOnlyDictionary<string, IList<string>> groups, string outputPath, BuildTarget target, BuildAssetBundleOptions options = BuildAssetBundleOptions.None)
        {
            if (groups == null) throw new ArgumentNullException(nameof(groups));
            if (string.IsNullOrEmpty(outputPath)) throw new ArgumentException("Value cannot be null or empty.", nameof(outputPath));

            var builds = new AssetBundleBuild[groups.Count];
            int index = 0;

            foreach (KeyValuePair<string, IList<string>> pair in groups)
            {
                var build = new AssetBundleBuild
                {
                    assetBundleName = pair.Key,
                    addressableNames = new string[pair.Value.Count],
                    assetNames = new string[pair.Value.Count]
                };

                for (int i = 0; i < pair.Value.Count; i++)
                {
                    string guid = pair.Value[i];
                    string path = AssetDatabase.GUIDToAssetPath(guid);

                    build.addressableNames[i] = guid;
                    build.assetNames[i] = path;
                }

                builds[index++] = build;
            }

            AssetBundleManifest manifest = BuildPipeline.BuildAssetBundles(outputPath, builds, options, target);

            return manifest;
        }

        public static AssetBundleEditorInfo LoadInfo(string path)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentException("Value cannot be null or empty.", nameof(path));

            AssetBundle assetBundle = AssetBundle.LoadFromFile(path);

            try
            {
                string name = assetBundle.name;
                var assets = new List<AssetBundleEditorInfo.AssetInfo>();
                var dependencies = new List<string>();
                bool isStreamedSceneAssetBundle = assetBundle.isStreamedSceneAssetBundle;

                var serializedObject = new SerializedObject(assetBundle);
                SerializedProperty propertyPreloadTable = serializedObject.FindProperty("m_PreloadTable");
                SerializedProperty propertyDependencies = serializedObject.FindProperty("m_Dependencies");

                for (int i = 0; i < propertyPreloadTable.arraySize; i++)
                {
                    SerializedProperty propertyElement = propertyPreloadTable.GetArrayElementAtIndex(i);
                    Object value = propertyElement.objectReferenceValue;

                    if (value != null)
                    {
                        string assetName = value.name;
                        Type assetType = value.GetType();
                        var assetInfo = new AssetBundleEditorInfo.AssetInfo(assetName, assetType);

                        assets.Add(assetInfo);
                    }
                }

                for (int i = 0; i < propertyDependencies.arraySize; i++)
                {
                    SerializedProperty propertyElement = propertyDependencies.GetArrayElementAtIndex(i);

                    dependencies.Add(propertyElement.stringValue);
                }

                BuildPipeline.GetCRCForAssetBundle(path, out uint crc);

                return new AssetBundleEditorInfo(name, crc, assets, dependencies, isStreamedSceneAssetBundle);
            }
            finally
            {
                if (assetBundle != null)
                {
                    assetBundle.Unload(true);
                }
            }
        }
    }
}
