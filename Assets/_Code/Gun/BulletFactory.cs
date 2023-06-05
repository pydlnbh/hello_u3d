using Comm;

using System;
using System.Collections;

using UnityEngine;

namespace Gun
{
    /// <summary>
    /// 子弹工厂类
    /// </summary>
    public static class BulletFactory
    {
        /// <summary>
        /// 创建新子弹
        /// </summary>
        /// <param name="bundleName">捆绑包名称</param>
        /// <param name="assetName">资产名称</param>
        /// <returns>子弹创建请求</returns>
        public static BulletCreateRequest CreateNewBullet(string bundleName, string assetName)
        {
            return new BulletCreateRequest(bundleName, assetName);
        }

        /// <summary>
        /// 子弹创建请求
        /// </summary>
        public sealed class BulletCreateRequest : IEnumerator
        {
            /**
             * 预制体加载请求
             */
            private readonly PrefabLoadRequest _prefabLoadReq;

            /**
             * 新创建的子弹
             */
            private GameObject _goBullet;

            /**
             * 完成回调函数
             */
            private Action<BulletCreateRequest> _onComplete = null;

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
            public BulletCreateRequest(string bundleName, string assetName)
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

                _goBullet = GameObject.Instantiate(goPrefab);
                _goBullet.SetActive(true);

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
            public Action<BulletCreateRequest> OnComplete
            {
                get => _onComplete;
                set
                {
                    _onComplete = value;

                    if (null != GetBullet() 
                     && !_completeFuncHasBeenCalled
                     && null != _onComplete)
                    {
                        _completeFuncHasBeenCalled = true;
                        _onComplete.Invoke(this);
                    }
                }
            }

            /// <summary>
            /// 获得子弹
            /// </summary>
            /// <returns>子弹游戏对象</returns>
            public GameObject GetBullet() => _goBullet;
        }
    }
}
