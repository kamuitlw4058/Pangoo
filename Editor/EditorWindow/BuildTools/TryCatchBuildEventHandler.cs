using System.Collections;
using System.Collections.Generic;
using Pangoo.Editor;
using UnityEditor;
using UnityEngine;
using UnityGameFramework.Editor.ResourceTools;
using GameFramework;

namespace  Pangoo
{
    

public class TryCatchBuildEventHandler : IBuildEventHandler
{
    public bool ContinueOnFailure
    {
        get { return false; }
    }

    public void OnPreprocessAllPlatforms(string productName, string companyName, string gameIdentifier,
        string gameFrameworkVersion, string unityVersion, string applicableGameVersion, int internalResourceVersion,
        Platform platforms, AssetBundleCompressionType assetBundleCompression, string compressionHelperTypeName,
        bool additionalCompressionSelected, bool forceRebuildAssetBundleSelected, string buildEventHandlerTypeName,
        string outputDirectory, BuildAssetBundleOptions buildAssetBundleOptions, string workingPath,
        bool outputPackageSelected, string outputPackagePath, bool outputFullSelected, string outputFullPath,
        bool outputPackedSelected, string outputPackedPath, string buildReportPath)
    {
    }

    public void OnPreprocessPlatform(Platform platform, string workingPath, bool outputPackageSelected,
        string outputPackagePath,
        bool outputFullSelected, string outputFullPath, bool outputPackedSelected, string outputPackedPath)
    {
    }

    public void OnBuildAssetBundlesComplete(Platform platform, string workingPath, bool outputPackageSelected,
        string outputPackagePath, bool outputFullSelected, string outputFullPath, bool outputPackedSelected,
        string outputPackedPath, AssetBundleManifest assetBundleManifest)
    {
    }

    public void OnOutputUpdatableVersionListData(Platform platform, string versionListPath, int versionListLength,
        int versionListHashCode, int versionListCompressedLength, int versionListCompressedHashCode)
    {
    }

    public void OnPostprocessPlatform(Platform platform, string workingPath, bool outputPackageSelected,
        string outputPackagePath,
        bool outputFullSelected, string outputFullPath, bool outputPackedSelected, string outputPackedPath,
        bool isSuccess)
    {
        if (!isSuccess)
        {
            BuildProject.isBuildFail = true;
            Debug.Log($"资源包打包失败:{BuildProject.isBuildFail}");
        }
    }

    public void OnPostprocessAllPlatforms(string productName, string companyName, string gameIdentifier,
        string gameFrameworkVersion, string unityVersion, string applicableGameVersion, int internalResourceVersion,
        Platform platforms, AssetBundleCompressionType assetBundleCompression, string compressionHelperTypeName,
        bool additionalCompressionSelected, bool forceRebuildAssetBundleSelected, string buildEventHandlerTypeName,
        string outputDirectory, BuildAssetBundleOptions buildAssetBundleOptions, string workingPath,
        bool outputPackageSelected, string outputPackagePath, bool outputFullSelected, string outputFullPath,
        bool outputPackedSelected, string outputPackedPath, string buildReportPath)
    {
    }
}
}