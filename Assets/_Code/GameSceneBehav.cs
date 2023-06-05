using System.Collections;

using UnityEngine;

/// <summary>
/// 游戏场景行为
/// </summary>
public sealed class GameSceneBehav : MonoBehaviour
{
    /**
     * 输入策略
     */
    private readonly AbstractInputStrategy _inputStrategy = new TouchInputStrategy();

    /**
     * 敌机节点
     */
    public GameObject _enemy_1;

    /// <summary>
    /// Start
    /// </summary>
    void Start()
    {
        StartCoroutine(CreateEnemy_XC());

        var bossBehav = GameObject.Find("/Boss_1").GetComponent<BossBehav>();
        bossBehav.SubtractHpEventHandler = (subtractHpEvent) =>
        {
            if (null == subtractHpEvent)
            {
                return;
            }

            int currHp = subtractHpEvent.CurrHp;
            int maxHp = subtractHpEvent.MaxHp;
            float widthRatio = (float)currHp / maxHp;

            var rtHpSlot = GameObject.Find("/Canvas/Img_HpSlot").transform as RectTransform;
            var rtHpVal = rtHpSlot.Find("Img_HpVal") as RectTransform;

            var leftMoveWidth = rtHpSlot.rect.width * (1f - widthRatio);

            var newPos = rtHpVal.anchoredPosition;
            newPos.Set(-leftMoveWidth, newPos.y);
            rtHpVal.anchoredPosition = newPos;
        };
    }

    /// <summary>
    /// Update
    /// </summary>
    void Update()
    {
        _inputStrategy.HandleInput();

        GameObject.Find("BGGroup/BG")
            .GetComponent<MeshRenderer>()
            .material.SetTextureOffset("_MainTex", Vector2.up * (Time.time / 20f));
    }

    /// <summary>
    /// 通过协程方式创建敌机
    /// </summary>
    /// <returns>协程用的枚举迭代器</returns>
    private IEnumerator CreateEnemy_XC()
    {
        while (true)
        {
            // 先准备 5 秒
            //yield return new WaitForSeconds(5f);

            for (var i = 0; i < 100; i++)
            {
                yield return new WaitForSeconds(Random.Range(0f, 0.5f));

                var enemyCreateReq = EnemyFactory.CreateNewEnemy(
                    "_Bundle.Out/enemy", 
                    "Assets/_Bundle.Src/enemy/Prefab/Enemy_1.prefab"
                );

                yield return enemyCreateReq;

                var goNewEnemy = enemyCreateReq.GetNewEnemy();

                goNewEnemy.transform.position += Vector3.right * Random.Range(-4f, +4f);
            }
        }
    }
}
