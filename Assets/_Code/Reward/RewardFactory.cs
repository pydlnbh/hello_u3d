using Comm;

using System;
using System.Collections;

using UnityEngine;

namespace Reward
{
    /// <summary>
    /// 奖励工厂类
    /// </summary>
    public static class RewardFactory
    {
        /// <summary>
        /// 创建新奖励
        /// </summary>
        /// <param name="bundleName">捆绑包名称</param>
        /// <param name="assetName">资产名称</param>
        /// <returns>子弹创建请求</returns>
        public static RewardCreateRequest CreateNewReward(string bundleName, string assetName)
        {
            return new RewardCreateRequest(bundleName, assetName);
        }

        /// <summary>
        /// 奖励创建请求
        /// </summary>
        public sealed class RewardCreateRequest : IEnumerator
        {
            /**
             * 预制体加载请求
             */
            private readonly PrefabLoadRequest _prefabLoadReq;

            /**
             * 新创建的奖励
             */
            private GameObject _goReward;

            /**
             * 完成回调函数
             */
            private Action<RewardCreateRequest> _onComplete = null;

            /**
             * 完成回调函数是否被调用过?
             */
            private bool _completeFuncHasBeenCalled = false;

            /// <summary>
            /// 类参数构造器
            /// </summary>
            /// <param name="bundleName">捆绑包名称</param>
            /// <param name="assetName">资产名称</param>
            /// <exception cref="System.ArgumentNullException">if bundleName is null or empty</exception>
            /// <exception cref="System.ArgumentNullException">if assetName is null or empty</exception>
            public RewardCreateRequest(string bundleName, string assetName)
            {
                if (string.IsNullOrEmpty(bundleName)
                 || string.IsNullOrEmpty(assetName))
                {
                    throw new System.ArgumentNullException();
                }

                _prefabLoadReq = new PrefabLoadRequest(bundleName, assetName);
                _prefabLoadReq.OnComplete += (_) =>
                {
                    MoveNext();
                };

                MoveNext();
            }

            // @Override
            public object Current => 1;

            // @Override
            public bool MoveNext()
            {
                if (_prefabLoadReq.MoveNext())
                {
                    return true;
                }

                var goPrefab = _prefabLoadReq.GetPrefab();

                if (null == goPrefab)
                {
                    return false;
                }

                _goReward = GameObject.Instantiate(goPrefab);
                _goReward.SetActive(true);

                if (!_completeFuncHasBeenCalled 
                  && null != OnComplete)
                {
                    _completeFuncHasBeenCalled = true;
                    OnComplete.Invoke(this);
                }

                return false;
            }

            // @Override
            public void Reset()
            {
            }

            /// <summary>
            /// 完成回调函数
            /// </summary>
            public Action<RewardCreateRequest> OnComplete
            {
                get => _onComplete;
                set
                {
                    _onComplete = value;

                    if (null != GetReward() 
                     && !_completeFuncHasBeenCalled
                     && null != _onComplete)
                    {
                        _completeFuncHasBeenCalled = true;
                        _onComplete.Invoke(this);
                    }
                }
            }

            /// <summary>
            /// 获得奖励
            /// </summary>
            /// <returns>奖励游戏对象</returns>
            public GameObject GetReward() => _goReward;
        }
    }
}
