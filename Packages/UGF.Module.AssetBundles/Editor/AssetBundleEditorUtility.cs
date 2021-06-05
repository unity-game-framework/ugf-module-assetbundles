using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Profiling;
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
                long size = Profiler.GetRuntimeMemorySizeLong(assetBundle);

                var serializedObject = new SerializedObject(assetBundle);
                SerializedProperty propertyPreloadTable = serializedObject.FindProperty("m_PreloadTable");
                SerializedProperty propertyContainer = serializedObject.FindProperty("m_Container");
                SerializedProperty propertyDependencies = serializedObject.FindProperty("m_Dependencies");

                var addresses = new Dictionary<int, string>();

                for (int i = 0; i < propertyContainer.arraySize; i++)
                {
                    SerializedProperty propertyElement = propertyContainer.GetArrayElementAtIndex(i);
                    SerializedProperty propertyFirst = propertyElement.FindPropertyRelative("first");
                    SerializedProperty propertySecond = propertyElement.FindPropertyRelative("second");
                    SerializedProperty propertyAsset = propertySecond.FindPropertyRelative("asset");

                    string address = propertyFirst.stringValue;
                    int instanceId = propertyAsset.objectReferenceInstanceIDValue;

                    if (instanceId != 0)
                    {
                        addresses[instanceId] = address;
                    }
                }

                for (int i = 0; i < propertyPreloadTable.arraySize; i++)
                {
                    SerializedProperty propertyElement = propertyPreloadTable.GetArrayElementAtIndex(i);
                    Object asset = propertyElement.objectReferenceValue;
                    int instanceId = propertyElement.objectReferenceInstanceIDValue;

                    if (asset != null)
                    {
                        string assetName = asset.name;
                        Type assetType = asset.GetType();
                        string address = addresses.TryGetValue(instanceId, out string value) ? value : string.Empty;
                        long assetSize = Profiler.GetRuntimeMemorySizeLong(asset);
                        var assetInfo = new AssetBundleEditorInfo.AssetInfo(assetName, assetType, address, assetSize);

                        assets.Add(assetInfo);
                    }
                }

                for (int i = 0; i < propertyDependencies.arraySize; i++)
                {
                    SerializedProperty propertyElement = propertyDependencies.GetArrayElementAtIndex(i);

                    dependencies.Add(propertyElement.stringValue);
                }

                BuildPipeline.GetCRCForAssetBundle(path, out uint crc);

                return new AssetBundleEditorInfo(name, crc, assets, dependencies, isStreamedSceneAssetBundle, size);
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
