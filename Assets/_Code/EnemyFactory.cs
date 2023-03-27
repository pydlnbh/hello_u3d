
using Comm;
using System.Collections;
using System.IO;
using UnityEngine;

/// <summary>
/// 敌机的工厂类
/// </summary>
public static class EnemyFactory
{


    public static EnemyCreateRequest CreateEnemy(string bundleName, string assetName)
    {
        return new EnemyCreateRequest(bundleName, assetName);
    }

    public class EnemyCreateRequest : IEnumerator
    {
        /**
         * 预制体加载请求
         */
        private readonly PrefabLoadRequest _prefabLoadReq;

        /**
         * 已创建的敌机
         */
        private GameObject _goNewEnemy;

        /// <summary>
        /// 类参数构造器
        /// </summary>
        /// <param name="bundleName">捆绑包名称</param>
        /// <param name="assetName">资产名称</param>
        public EnemyCreateRequest(string bundleName, string assetName)
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

            // 创建复制体
            _goNewEnemy = GameObject.Instantiate(goPrefab);
            _goNewEnemy.SetActive(true);

            return false;
        }

        public void Reset()
        {
        }

        /// <summary>
        /// 返回已经创建的新敌机
        /// </summary>
        /// <returns>敌机的游戏对象</returns>
        public GameObject GetNewEnemy() => _goNewEnemy;
    }
}
