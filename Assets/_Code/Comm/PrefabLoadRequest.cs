using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace Comm
{
    public sealed class PrefabLoadRequest : IEnumerator
    {
        /**
         * 以加载的捆绑包字典
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
        private Action<PrefabLoadRequest> _completed = null;

        /**
         * 回调函数调用标识
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

        public object Current => 1;

        public bool MoveNext()
        {
            LoadAb();
            LoadPrefab();

            if (1 != _prefabLoadState)
            {
                return true;
            }

            if (!_completeFuncHasBeenCalled
             && null != completed)
            {
                _completeFuncHasBeenCalled = true;
                completed.Invoke(this);
            }

            return false;
        }

        public void LoadAb()
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
                return;
            }

            // 设置正在加载的状态
            abLoadEntry.AbLoadState = 0;

#if UNITY_ANDROID
            // jar:file://xxx.apk!//assets/Xxx
            var abPath = Path.Combine(Application.streamingAssetsPath, _bundleName);

            var webReq = UnityWebRequest.Get(abPath);
            var op = webReq.SendWebRequest();

            op.completed += (_) =>
            {
                var data = webReq.downloadHandler.data;
                var op2 = AssetBundle.LoadFromMemoryAsync(data);

                op2.completed += (_) =>
                {
                    abLoadEntry.Ab = op2.assetBundle;
                    abLoadEntry.AbLoadState = 1;

                    MoveNext();
                };
            };

#elif UNITY_EDITOR
            // 类似加载一个 zip 文件
            var abPath = Path.Combine(Application.dataPath, _bundleName);
            var abCreateRequest = AssetBundle.LoadFromFileAsync(abPath);
            abCreateRequest.completed += (_) =>
            {
                // 拿到 zip 文件了, 还得拿到 zip 文件里面的某个文件
                abLoadEntry.Ab = abCreateRequest.assetBundle;
                abLoadEntry.AbLoadState = 1;

                MoveNext();
            };
#endif
        }

        /// <summary>
        /// 加载预制体
        /// </summary>
        public void LoadPrefab()
        {
            if (_goPrefab != null)
            {
                _prefabLoadState = 1;
                return;
            }

            if (-1 != _prefabLoadState)
            {
                // 已经处在加载中的状态，直接退出
                return;
            }

            if (!_abDict.TryGetValue(_bundleName, out var abLoadEntry)
             || 1 != abLoadEntry.AbLoadState)
            {
                return;
            }

            if (null == abLoadEntry.Ab)
            {
                _prefabLoadState = 1;
                return;
            }

            _prefabLoadState = 0;

            var abRequest = abLoadEntry.Ab.LoadAssetAsync<GameObject>(_assetName);
            abRequest.completed += (_) =>
            {
                _goPrefab = abRequest.asset as GameObject;
                _prefabLoadState = 1;

                MoveNext();
            };
        }

        public void Reset()
        {
        }

        /// <summary>
        /// 回调方法
        /// </summary>
        public Action<PrefabLoadRequest> completed
        {
            get => _completed;
            set
            {
                _completed = value;

                if (null != GetPrefab()
                 && !_completeFuncHasBeenCalled
                 && null != _completed)
                {
                    _completeFuncHasBeenCalled = true;
                    _completed.Invoke(this);
                }
            }
        }

        /// <summary>
        /// 返回已经加载的预制体
        /// </summary>
        /// <returns>加载的预制体</returns>
        public GameObject GetPrefab() => _goPrefab;

        /// <summary>
        /// 捆绑包的加载项
        /// </summary>
        private sealed class AbLoadEntry
        {
            public int AbLoadState
            {
                get;
                set;
            }

            public AssetBundle Ab
            {   
                get;
                set; 
            }
        }
    }
}
