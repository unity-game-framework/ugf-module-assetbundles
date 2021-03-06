# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [2.0.0-preview](https://github.com/unity-game-framework/ugf-module-assetbundles/releases/tag/2.0.0-preview) - 2022-07-14  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-assetbundles/milestone/11?closed=1)  
    

### Changed

- Change string ids to global id data ([#34](https://github.com/unity-game-framework/ugf-module-assetbundles/issues/34))  
    - Update dependencies: `com.ugf.application` to `8.3.0` and `com.ugf.module.assets` to `5.0.0-preview` versions.
    - Update package _Unity_ version to `2022.1`.
    - Change usage of ids as `GlobalId` structure instead of `string`.

## [1.1.0](https://github.com/unity-game-framework/ugf-module-assetbundles/releases/tag/1.1.0) - 2022-07-07  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-assetbundles/milestone/10?closed=1)  
    

### Added

- Add building all bundles from project settings ([#30](https://github.com/unity-game-framework/ugf-module-assetbundles/issues/30))  
    - Update dependencies: `com.ugf.application` to `8.2.0` version.
    - Add `AssetBundleEditorSettings` static class to access _Asset Bundles_ project settings.
    - Add `AssetBundleEditorSettings.BuildAll()` and `ClearAll()` methods to build and clear list of the specified asset bundles from project settings.
    - Add `AssetBundleBuildEditorUtility.BuildAll()` and `ClearAll()` methods to build and clear list of the specified asset bundles.
    - Add `AssetBundleBuildEditorUtility.Clear()` method to clear specified asset bundle.

## [1.0.0](https://github.com/unity-game-framework/ugf-module-assetbundles/releases/tag/1.0.0) - 2022-05-18  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-assetbundles/milestone/9?closed=1)  
    

### Added

- Add option to delete manifest files after build ([#27](https://github.com/unity-game-framework/ugf-module-assetbundles/issues/27))  
    - Update dependencies: `com.ugf.assetbundles` to `1.0.0` version.
    - Add `AssetBundleBuildAsset.ClearManifests` property as option to clear manifest files after _AssetBundle_ build.

### Fixed

- Fix asset bundle asset loader does not using type ([#26](https://github.com/unity-game-framework/ugf-module-assetbundles/issues/26))  
    - Update dependencies: `com.ugf.application` to `8.1.0`,  `com.ugf.module.assets` to `4.0.0` and `com.ugf.assetbundles` to `1.0.0-preview.2`.
    - Update package _Unity_ version to `2021.3`.
    - Update package _API Compatibility_ level to `.NET Standard 2.1`.
    - Change `AssetBundleAssetLoader` class to use specified type argument to load asset.
    - Change `AssetBundleAssetLoader` class to throw exceptions when no asset loaded.
    - Change `AssetBundleFileLoader` class to throw exceptions when no asset bundle loaded.
    - Change inspector editor to use latest updates, and selection preview for some collections.
    - Remove `AssetBundleLoader` class as duplicated, use `AssetBundleFileLoader` class instead.

## [1.0.0-preview.7](https://github.com/unity-game-framework/ugf-module-assetbundles/releases/tag/1.0.0-preview.7) - 2021-08-07  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-assetbundles/milestone/8?closed=1)  
    

### Changed

- Update package UGF.AssetBundles ([#24](https://github.com/unity-game-framework/ugf-module-assetbundles/pull/24))  
    - Update dependencies: `com.ugf.assetbundles` to `1.0.0-preview.1` version.

## [1.0.0-preview.6](https://github.com/unity-game-framework/ugf-module-assetbundles/releases/tag/1.0.0-preview.6) - 2021-08-06  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-assetbundles/milestone/7?closed=1)  
    

### Removed

- Remove AssetBundleEditorUtility class ([#22](https://github.com/unity-game-framework/ugf-module-assetbundles/pull/22))  
    - Add `AssetBundleBuildEditorUtility.GetAssetBundleBuilds()` method to get list of `AssetBundleBuildInfo` classes used to build assetbundles.
    - Remove `AssetBundleEditorUtility` class, use `AssetBundleBuildUtility` class from _UGF.AssetBundles_ package instead.

## [1.0.0-preview.5](https://github.com/unity-game-framework/ugf-module-assetbundles/releases/tag/1.0.0-preview.5) - 2021-08-04  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-assetbundles/milestone/6?closed=1)  
    

### Changed

- Add usage of ugf-assetbundles package ([#20](https://github.com/unity-game-framework/ugf-module-assetbundles/pull/20))  
    - Update dependencies: add `com.ugf.assetbundles` of `1.0.0-preview` version.
    - Remove `AssetBundleEditorInfoContainer` and related classes, use _AssetBundleFileDrawer_ from _UGF.AssetBundles_ package to draw assetbundle information instead.
    - Remove `AssetBundleEditorUtility.LoadInfo` method use similar method from _UGF.AssetBundles_ package instead.

## [1.0.0-preview.4](https://github.com/unity-game-framework/ugf-module-assetbundles/releases/tag/1.0.0-preview.4) - 2021-07-27  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-assetbundles/milestone/5?closed=1)  
    

### Changed

- Change asset bundle editor info container to be public ([#17](https://github.com/unity-game-framework/ugf-module-assetbundles/pull/17))  
    - Add `AssetBundleEditorInfoContainerUtility.CreateContainer()` method with path as argument to create container based on asset bundle at specific path.
    - Add `AssetBundleEditorInfoContainerUtility.CreateContainer()` method to create container based on `AssetBundleEditorInfo` information.
    - Change `AssetBundleEditorInfoContainer` class to be public.

### Removed

- Remove asset bundle editor info size information ([#18](https://github.com/unity-game-framework/ugf-module-assetbundles/pull/18))  
    - Remove size information from `AssetBundleEditorInfo` and `AssetBundleEditorInfoContainer` classes, because it provides wrong information not related to asset bundle size.
    - Remove `AssetBundleEditorInfo.Size` and `AssetBundleEditorInfo.AssetInfo.Size` properties.
    - Remove `AssetBundleEditorInfoContainer.Size` and `AssetBundleEditorInfoContainer.AssetInfo.Size` properties.

## [1.0.0-preview.3](https://github.com/unity-game-framework/ugf-module-assetbundles/releases/tag/1.0.0-preview.3) - 2021-07-12  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-assetbundles/milestone/4?closed=1)  
    

### Changed

- Replace AssetBundleFileStorageDirectory with StoragePathType from runtime tools package ([#14](https://github.com/unity-game-framework/ugf-module-assetbundles/pull/14))  
    - Update dependencies: `com.ugf.runtimetools` to `2.1.0` version.
    - Change `AssetBundleFileStorageAsset` to use `StoragePathType` enumerable from _UGF.RuntimeTools_ package to specify directory.
    - Remove `AssetBundleFileStorageDirectory` enumerable and `AssetBundleFileStorageUtility` class, use `StoragePathType` and `StorageUtility` class from _UGF.RuntimeTools_ package instead.

## [1.0.0-preview.2](https://github.com/unity-game-framework/ugf-module-assetbundles/releases/tag/1.0.0-preview.2) - 2021-06-12  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-assetbundles/milestone/3?closed=1)  
    

### Fixed

- Fix asset bundle module to unload known bundles using default parameters of loader ([#12](https://github.com/unity-game-framework/ugf-module-assetbundles/pull/12))  
    - Fix asset bundle module to unload known bundles on uninitialization using default unload parameters from loader.

## [1.0.0-preview.1](https://github.com/unity-game-framework/ugf-module-assetbundles/releases/tag/1.0.0-preview.1) - 2021-06-11  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-assetbundles/milestone/2?closed=1)  
    

### Added

- Add default load and unload parameters for loader ([#10](https://github.com/unity-game-framework/ugf-module-assetbundles/pull/10))  
    - Update dependencies: `com.ugf.module.assets` to `4.0.0-preview.3` version.
    - Add `AssetBundleLoader`, `AssetBundleAssetLoader` and `AssetBundleFileLoader` constructor with default load and unload parameters.
    - Add `AssetBundleAssetLoaderAsset` and `AssetBundleFileLoaderAsset` serializable load and unload parameters.
    - Change `AssetBundleLoadParameters`, `AssetBundleUnloadParameters`, `AssetBundleAssetLoadParameters` and `AssetBundleAssetUnloadParameters` parameter classes to be serializable.

## [1.0.0-preview](https://github.com/unity-game-framework/ugf-module-assetbundles/releases/tag/1.0.0-preview) - 2021-06-07  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-assetbundles/milestone/1?closed=1)  
    

### Added

- Add asset bundle file viewer ([#7](https://github.com/unity-game-framework/ugf-module-assetbundles/issues/7))  
    - Viewer to display internal asset bundle information.
    - Tools to load and work with asset bundle file information.
- Add editor asset bundle building tools ([#4](https://github.com/unity-game-framework/ugf-module-assetbundles/issues/4))  
    - Add asset bundle building tools in editor.
    - Build asset bundles with regular dependencies.
- Create package ([#2](https://github.com/unity-game-framework/ugf-module-assetbundles/pull/2))  
    - Create AssetBundleModue and related classes.


