# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

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


