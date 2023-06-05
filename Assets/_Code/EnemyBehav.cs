using Gun;

using Reward;

using System.Collections;

using UnityEngine;

/// <summary>
/// 敌机行为
/// </summary>
public sealed class EnemyBehav : MonoBehaviour
{
    /**
     * 移动速度
     */
    public float MV_SPEED = 10f;

    /**
     * 当前血量
     */
    public int _currHp = 10;

    /**
     * 最后一次被攻击的时间
     */
    private float _lastBeHitTime = -1f;

    /**
     * 抖动的枚举迭代器
     */
    private IEnumerator _shakeEnum = null;

    /// <summary>
    /// Start
    /// </summary>
    void Start()
    {   
    }

    /// <summary>
    /// Update
    /// </summary>
    void Update()
    {
        _shakeEnum?.MoveNext();

        if (Time.time - _lastBeHitTime <= 0.05f)
        {
            gameObject.GetComponent<SpriteRenderer>()
                .color = Color.red;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>()
                .color = Color.white;
        }

        //gameObject.transform.Translate(Vector3.up * (MV_SPEED * Time.deltaTime));
        gameObject.transform.position += Vector3.down * (MV_SPEED * Time.deltaTime);

        if (gameObject.transform.position.y < -30f)
        {
            // 如果已经飞出屏幕外,
            // 直接销毁
            GameObject.Destroy(gameObject);
        }
    }

    /// <summary>
    /// 碰撞侦测函数
    /// </summary>
    /// <param name="c">另外一个碰撞盒</param>
    private void OnCollisionEnter(Collision c)
    {
        if (null == c 
         || c.gameObject.name.StartsWith("Enemy_"))
        {
            return;
        }

        //Debug.Log("被射了...");
        //Destroy(c.gameObject);

        if (null == _shakeEnum)
        {
            _shakeEnum = DoShake();
        }

        gameObject.transform.Find("PSys_0")
            .gameObject.GetComponent<ParticleSystem>()
            .Play();

        var bulletBehav = c.gameObject.GetComponent<AbstractBullet>();
        _currHp -= bulletBehav.GetDmg();

        _lastBeHitTime = Time.time;

        if (_currHp <= 0)
        {
            var goPSys = gameObject.transform
                .Find("PSys_ForDestroy")
                .gameObject;

            // 复制一份粒子系统, 免得在敌机销毁时一起被销毁.
            // 其实更好的办法应该是做个池子 Pool
            var goNewPSys = GameObject.Instantiate(goPSys);

            goNewPSys.transform.SetPositionAndRotation(
                gameObject.transform.position,
                Quaternion.identity
            );

            goNewPSys.GetComponent<ParticleSystem>().Play();

            if (Random.Range(0, 100) <= 10)
            {
                // 10% 掉落奖励
                //
                // 随机一个奖励类型
                var bulletType = Random.Range(1, 3); // [1, 3) => 1, 2

                var rewardAtWorldPos = gameObject.transform.position;

                var req = RewardFactory.CreateNewReward(
                    "_Bundle.Out/reward",
                    $"Assets/_Bundle.Src/reward/Prefab/Reward_Bullet_{bulletType}.prefab"
                );

                req.OnComplete += (_) =>
                {
                    var goReward = req.GetReward();
                    goReward.transform.position = rewardAtWorldPos;
                };
            }

            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 通过枚举迭代器的方式执行抖动
    /// </summary>
    /// <returns>枚举迭代器</returns>
    private IEnumerator DoShake()
    {
        var currPosXZY = gameObject.transform.position;
        var currPosX = currPosXZY.x;
        var lX = currPosX - 1f;
        var rX = currPosX + 1f;

        // 往左抖
        while (gameObject.transform.position.x > lX)
        {
            gameObject.transform.position += Vector3.left * (Time.deltaTime * 16f);
            yield return 1;
        }

        // 往右抖
        while (gameObject.transform.position.x < rX)
        {
            gameObject.transform.position += Vector3.right * (Time.deltaTime * 16f);
            yield return 1;
        }

        // 回到原位
        while (gameObject.transform.position.x > currPosX)
        {
            gameObject.transform.position += Vector3.left * (Time.deltaTime * 10f);
            yield return 1;
        }

        //gameObject.transform.position = currPosXZY;
        _shakeEnum = null;
    }
}
