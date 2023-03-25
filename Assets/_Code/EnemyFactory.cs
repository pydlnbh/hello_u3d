
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
        private static AssetBundle _ab;

        /**
         * 捆绑包名称
         */
        private readonly string _bundleName;

        /**
         * 资产名称
         */
        private readonly string _assetName;

        /**
         * 已经创建的敌机对象
         */
        private GameObject _goNewEnemy;

        /**
         * 捆绑包加载状态
         */
        private int _abLoadState = -1;

        /**
         * 敌机创建状态
         */
        private int _newEnemyCreateState = -1;

        public EnemyCreateRequest(string bundleName, string assetName)
        {
            _bundleName = bundleName;
            _assetName = assetName;
        }

        public object Current => 1;

        public bool MoveNext()
        {
            LoadAb();
            CreateNewEnemy();
            return _newEnemyCreateState != 1;
        }

        public void LoadAb()
        {
            if (null != _ab)
            {
                _abLoadState = 1;
                return;
            }

            if (-1 != _abLoadState)
            {
                return;
            }

            // 设置正在加载的状态
            _abLoadState = 0;

            // 类似加载一个 zip 文件
            var abPath = Path.Combine(Application.dataPath, _bundleName);
            var abCreateRequest = AssetBundle.LoadFromFileAsync(abPath);
            abCreateRequest.completed += (_) =>
            {
                // 拿到 zip 文件了, 还得拿到 zip 文件里面的某个文件
                _ab = abCreateRequest.assetBundle;
                _abLoadState = 1;
            };
        }

        public void CreateNewEnemy()
        {
            if (_goNewEnemy != null)
            {
                _newEnemyCreateState = 1;
                return;
            }

            if (-1 != _newEnemyCreateState)
            {
                // 已经处在创建中的状态，直接退出
                return;
            }

            if (1 != _abLoadState)
            {
                return;
            }

            if (null == _ab)
            {
                _newEnemyCreateState = 1;
                return;
            }

            _newEnemyCreateState = 0;
            
            var abRequest = _ab.LoadAssetAsync<GameObject>(_assetName);
            abRequest.completed += (_) =>
            {
                var goPrefab = abRequest.asset as GameObject;

                // 创建复制体
                GameObject goNewEnemy = GameObject.Instantiate(goPrefab);
                goNewEnemy.SetActive(true);

                _goNewEnemy = goNewEnemy;
                _newEnemyCreateState = 1;
            };
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
