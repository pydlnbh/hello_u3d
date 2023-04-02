using Comm;
using System;
using System.Collections;
using UnityEngine;

namespace Gun
{
    public static class BulletFactory
    {
        public static BulletCreateRequest createNewBullet(string bundleName, string assetName)
        {
            return new BulletCreateRequest(bundleName, assetName);
        }

        /// <summary>
        /// 类参数构造器
        /// </summary>
        /// <param name="bundleName">捆绑包名称</param>
        /// <param name="assetName">资产名称</param>
        public class BulletCreateRequest : IEnumerator
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
            private Action<BulletCreateRequest> _completed = null;

            /**
             * 回调函数调用标识
             */
            private bool _completeFuncHasBeenCalled = false;

            public BulletCreateRequest(string bundleName, string assetName)
            {
                if (string.IsNullOrEmpty(bundleName)
                 || string.IsNullOrEmpty(assetName))
                {
                    throw new System.ArgumentNullException();
                }

                _prefabLoadReq = new PrefabLoadRequest(bundleName, assetName);
                _prefabLoadReq.completed += (_) =>
                {
                    MoveNext();
                };

                MoveNext();
            }

            public object Current => 1;

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
                 && null != completed)
                {
                    _completeFuncHasBeenCalled = true;
                    completed.Invoke(this);
                }

                return false;
            }

            public void Reset()
            {
            }

            /// <summary>
            /// 回调方法
            /// </summary>
            public Action<BulletCreateRequest> completed
            {
                get => _completed;
                set
                {
                    _completed = value;

                    if (null != GetBullet() 
                     && !_completeFuncHasBeenCalled 
                     && null != _completed)
                    {
                        _completeFuncHasBeenCalled = true;
                        _completed.Invoke(this);
                    }
                }
            }

            public GameObject GetBullet() => _goBullet;
        }
    }
}
