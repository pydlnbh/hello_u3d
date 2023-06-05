using UnityEditor;

namespace Editor
{
    [InitializeOnLoad]
    public sealed class Editor_OnStartUp
    {
        /// <summary>
        /// 类的静态构造器
        /// </summary>
        static Editor_OnStartUp()
        {
            EditorApplication.playModeStateChanged += (playModeStateChange) =>
            {
                if (PlayModeStateChange.ExitingEditMode == playModeStateChange)
                {
                    UnityEngine.Debug.Log("重新构建捆绑包");
                    // 当退出编辑状态, 重新构建 Bundle 捆绑包
                    CreateAssetBundles.BuildAllAssetBundles();
                }
            };
        }
    }
}
