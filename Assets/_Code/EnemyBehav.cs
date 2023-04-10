using Gun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class EnemyBehav : MonoBehaviour
{
    /**
     * 移动速度
     */
    public float MOVE_SPEED = 10f;

    /**
     * 当前血量
     */
    public int _curHP = 10;

    /**
     * 上一次攻击的时间
     */
    private float _lastTime = -1f;

    /**
     * 抖动枚举迭代器
     */
    private IEnumerator _shakeEnum = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 抖动
        _shakeEnum?.MoveNext();

        if (Time.time - _lastTime <= 0.05f)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }

        // gameObject.transform.Translate(Vector3.up * (MOVE_SPEED * Time.deltaTime));
        gameObject.transform.position += Vector3.down * (MOVE_SPEED * Time.deltaTime);

        if (gameObject.transform.position.y < -20f)
        {
            GameObject.Destroy(gameObject);
        }
    }

    /// <summary>
    /// 碰撞侦测函数
    /// </summary>
    /// <param name="collision">另一个碰撞盒</param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision == null ||
            collision.gameObject.name.StartsWith("Enemy_"))
        {
            return;
        }

        // Destroy(collision.gameObject);

        if (_shakeEnum == null)
        {
            _shakeEnum = DoShake();
        }

        gameObject.transform.Find("PSys_0")
            .gameObject.GetComponent<ParticleSystem>()
            .Play();

        _curHP--;
        _lastTime = Time.time;

        if (_curHP <= 0)
        {
            var goPSys = gameObject.transform.Find("PSys_ForDestory").gameObject;

            var goNewPSys = GameObject.Instantiate(goPSys);

            goNewPSys.transform.SetPositionAndRotation(gameObject.transform.position, Quaternion.identity);

            goNewPSys.GetComponent<ParticleSystem>().Play();

            if (Random.Range(0, 100) <= 10)
            {
                // [1, 3) => 1, 2
                var bulletType = Random.Range(1, 3);

                var rewardAtWorldPos = gameObject.transform.position;

                var req = RewardFactory.createNewReward(
                    "_Bundle.Out/reward",
                    $"Assets/_Bundle.Src/reward/Prefab/Reward_Bullet_{bulletType}.prefab"
                );

                req.completed += (_) =>
                {
                    var goReward = req.GetReward();
                    goReward.transform.position = rewardAtWorldPos;
                };
            }

            Destroy(gameObject);
        }
    }

    private IEnumerator DoShake()
    {
        var curPos = gameObject.transform.position;
        var curPosX = curPos.x;
        var lX = curPosX - 1f;
        var rX = curPosX + 1f;

        // 往左抖
        while (gameObject.transform.position.x > lX)
        {
            gameObject.transform.position += Vector3.left * Time.deltaTime * 16f;
            yield return 1;
        }

        // 往右抖
        while (gameObject.transform.position.x < rX)
        {
            gameObject.transform.position += Vector3.right * Time.deltaTime * 16f;
            yield return 1;
        }

        // 回到原位
        while (gameObject.transform.position.x > curPosX)
        {
            gameObject.transform.position += Vector3.left * Time.deltaTime * 16f;
            yield return 1;
        }

        // gameObject.transform.position = curPos;
        _shakeEnum = null;
    }
}
