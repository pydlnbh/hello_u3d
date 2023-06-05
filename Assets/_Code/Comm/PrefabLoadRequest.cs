using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;
using UnityEngine.Networking;

namespace Comm
{
    /// <summary>
    /// 预制体加载请求
    /// </summary>
    public sealed class PrefabLoadRequest : IEnumerator
    {
        /**
         * 已加载的捆绑包字典
         * key = bundleName
         */
        private static readonly Dictionary<string, AbLoadEntry> _abDict = new();

        /**
         * 捆绑包名称
         */
        private readonly string _bundleName;

        /**
         * 资产名称
         */
        private readonly string _assetName;

        /**
         * 预制体的加载状态
         * -1 = 未开始, 0 = 正在创建, 1 = 创建完成
         */
        private int _prefabLoadState = -1;

        /**
         * 已加载的预制体
         */
        private GameObject _goPrefab;

        /**
         * 完成回调函数
         */
        private Action<PrefabLoadRequest> _onComplete = null;

        /**
         * 完成回调函数是否被调用过?
         */
        private bool _completeFuncHasBeenCalled = false;

        /// <summary>
        /// 类参数构造器
        /// </summary>
        /// <param name="bundleName">捆绑包名称</param>
        /// <param name="assetName">资产名称</param>
        public PrefabLoadRequest(string bundleName, string assetName)
        {
            if (string.IsNullOrEmpty(bundleName)
             || string.IsNullOrEmpty(assetName))
            {
                throw new System.ArgumentNullException();
            }

            _bundleName = bundleName;
            _assetName = assetName;

            MoveNext();
        }

        // @Override
        public object Current => 1;

        // @Override
        public bool MoveNext()
        {
            LoadAb();
            LoadPrefab();

            if (1 != _prefabLoadState)
            {
                return true;
            }

            if (!_completeFuncHasBeenCalled
              && null != OnComplete)
            {
                _completeFuncHasBeenCalled = true;
                OnComplete.Invoke(this);
            }

            return false;
        }

        /// <summary>
        /// 记载捆绑包
        /// </summary>
        private void LoadAb()
        {
            if (!_abDict.TryGetValue(_bundleName, out var abLoadEntry))
            {
                abLoadEntry = new AbLoadEntry()
                {
                    AbLoadState = -1,
                    Ab = null,
                };
                _abDict[_bundleName] = abLoadEntry;
            }

            if (null != abLoadEntry.Ab)
            {
                abLoadEntry.AbLoadState = 1;
                return;
            }

            if (-1 != abLoadEntry.AbLoadState)
            {
                // 如果捆绑包加载状态不是 "未开始",
                // 那要么是已开始,
                // 要么是已完成,
                // 直接退出吧...
                return;
            }

            // 设置正在加载的状态
            abLoadEntry.AbLoadState = 0;

#if UNITY_ANDROID
            // 如果是 Android 系统,
            // 就需要通过 WebRequest 来读取!
            // 因为是 jar:file://Xxx.apk!/assets/Xxx 这样的 url,
            // 无法直接使用 LoadFromFile 函数
            var abPath = Path.Combine(Application.streamingAssetsPath, _bundleName);
            var webReq = UnityWebRequest.Get(abPath);

            var op = webReq.SendWebRequest();
            op.completed += (_) =>
            {
                var ab = AssetBundle.LoadFromMemory(webReq.downloadHandler.data);
                abLoadEntry.Ab = ab;
                abLoadEntry.AbLoadState = 1;

                MoveNext();
            };
#elif UNITY_EDITOR
            // 类似于加载一个 zip 文件
            var abPath = Path.Combine(Application.dataPath, _bundleName);

            var abCreateRequest = AssetBundle.LoadFromFileAsync(abPath);
            abCreateRequest.completed += (_) => // addListener
            {
                // 拿到 zip 文件了,
                // 那还得拿到 zip 文件里面的某个文件
                abLoadEntry.Ab = abCreateRequest.assetBundle;
                abLoadEntry.AbLoadState = 1;

                MoveNext();
            };
#endif
        }

        /// <summary>
        /// 加载预制体
        /// </summary>
        private void LoadPrefab()
        {
            if (null != _goPrefab)
            {
                _prefabLoadState = 1;
                return;
            }

            if (-1 != _prefabLoadState)
            {
                // 已经处于加载中的状态,
                // 就直接退出!
                return;
            }

            if (!_abDict.TryGetValue(_bundleName, out var abLoadEntry)
             || 1 != abLoadEntry.AbLoadState)
            {
                // 如果捆绑包还没加载完成呢,
                // 直接退出!
                return;
            }

            if (null == abLoadEntry.Ab)
            {
                // 这种情况只能是捆绑包加载失败了,
                // 这样也不能创建新敌机了...
                _prefabLoadState = 1;
                return;
            }

            // 设置为 "正在创建" 的状态
            _prefabLoadState = 0;

            var abRequest = abLoadEntry.Ab.LoadAssetAsync<GameObject>(_assetName);
            abRequest.completed += (_) =>
            {
                _goPrefab = abRequest.asset as GameObject;
                _prefabLoadState = 1;

                MoveNext();
            };
        }

        // @Override
        public void Reset()
        {
        }

        /// <summary>
        /// 返回已经加载的预制体
        /// </summary>
        /// <returns>经加载的预制体</returns>
        public GameObject GetPrefab() => _goPrefab;

        /// <summary>
        /// 完成回调函数
        /// </summary>
        public Action<PrefabLoadRequest> OnComplete
        {
            get => _onComplete;
            set
            {
                _onComplete = value;

                if (null != GetPrefab()
                 && !_completeFuncHasBeenCalled
                 && null != _onComplete)
                {
                    _completeFuncHasBeenCalled = true;
                    _onComplete.Invoke(this);
                }
            }
        }

        /// <summary>
        /// 捆绑包的加载项
        /// </summary>
        private sealed class AbLoadEntry
        {
            /// <summary>
            /// 捆绑包加载状态,
            /// -1 = 未开始, 0 = 正在加载, 1 = 加载完成
            /// </summary>
            public int AbLoadState
            {
                get;
                set;
            }

            /// <summary>
            /// 捆绑包
            /// </summary>
            public AssetBundle Ab
            {
                get;
                set;
            }
        }
    }
}
