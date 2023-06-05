using System.IO;
using UnityEditor;
using UnityEngine;

public class  CreateAssetBundles
{
    [MenuItem("Tools/Build AssetBundles")]
    public static void BuildAllAssetBundles()
    {
        string assetBundleDir = "Assets/_Bundle.Out";
        if (!Directory.Exists(assetBundleDir))
        {
            Directory.CreateDirectory(assetBundleDir);
        }
        BuildPipeline.BuildAssetBundles(assetBundleDir,
                                        BuildAssetBundleOptions.None,
                                        EditorUserBuildSettings.activeBuildTarget);

        var streamAssetsDir = "Assets/StreamingAssets/_Bundle.Out/";
        
        if (!Directory.Exists(streamAssetsDir))
        {
            Directory.CreateDirectory(streamAssetsDir);
        }

        var fileNameArr = Directory.GetFiles(assetBundleDir);

        foreach (var fileName in fileNameArr)
        {
            var copyToFileName = Path.GetFileName(fileName);
            copyToFileName = Path.Combine(streamAssetsDir, copyToFileName);

            File.Copy(fileName, copyToFileName);
        }
    }

    [MenuItem("Tools/Hi")]
    static void sayHi()
    {
        Debug.Log("hi");
    }
}