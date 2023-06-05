using System.IO;

using UnityEditor;

using UnityEngine;

/// <summary>
/// 创建捆绑包
/// </summary>
public sealed class CreateAssetBundles
{
    /// <summary>
    /// 扩展 Unity Editor, 
    /// 主菜单中增加 Tools/Build AssetBundles 子菜单
    /// </summary>
    [MenuItem("Tools/Build AssetBundles")]
    public static void BuildAllAssetBundles()
    {
        // 输出目录
        string assetBundleDir = "Assets/_Bundle.Out";

        if (!Directory.Exists(assetBundleDir))
        {
            Directory.CreateDirectory(assetBundleDir);
        }

        BuildPipeline.BuildAssetBundles(
            assetBundleDir,
            BuildAssetBundleOptions.None,
            EditorUserBuildSettings.activeBuildTarget
        );

        var streamingAssetsDir = "Assets/StreamingAssets/_Bundle.Out";

        if (!Directory.Exists(streamingAssetsDir))
        {
            Directory.CreateDirectory(streamingAssetsDir);
        }

        var fileNameArray = Directory.GetFiles(assetBundleDir);

        foreach (var fileName in fileNameArray)
        {
            var copyToFileName = Path.GetFileName(fileName);

            copyToFileName = Path.Combine(
                streamingAssetsDir, 
                copyToFileName
            );

            File.Copy(fileName, copyToFileName);
        }
    }

    // 这里是一个测试,
    // 主要是演示 Unity Editor 可以随意扩展
    [MenuItem("Tools/SayHello")]
    static void SayHello()
    {
        Debug.Log("Hello!");
    }
}