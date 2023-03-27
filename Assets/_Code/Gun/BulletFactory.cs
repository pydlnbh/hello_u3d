using Comm;
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

            public BulletCreateRequest(string bundleName, string assetName)
            {
                if (string.IsNullOrEmpty(bundleName)
                 || string.IsNullOrEmpty(assetName))
                {
                    throw new System.ArgumentNullException();
                }

                _prefabLoadReq = new PrefabLoadRequest(bundleName, assetName);
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

                return false;
            }

            public void Reset()
            {
            }

            public GameObject GetBullet() => _goBullet;
        }
    }
}
