using System.Collections;
using UnityEngine;

public class GameSceneBehav : MonoBehaviour
{
    /**
     * 敌机节点
     */
    public GameObject _enemy_1;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreateEnemy_XC());
    }

    // Update is called once per frame
    void Update()
    {
        GameObject.Find("BGGroup/BG")
            .GetComponent<MeshRenderer>()
            .material.SetTextureOffset("_MainTex", Vector2.up * (Time.time / 20f));
    }

    private IEnumerator CreateEnemy_XC()
    {
        while (true)
        {
            // 先准备 5 秒
            // yield return new WaitForSeconds(5f);

            for (var i = 0; i < 100; i++)
            {
                yield return new WaitForSeconds(Random.Range(0f, 0.5f));

                var enemyCreateReqest = EnemyFactory.CreateEnemy(
                    "_Bundle.out/enemy",
                    "Assets/_Bundle.src/enemy/Prefab/Enemy_1.prefab"
                );

                yield return enemyCreateReqest;

                var goNewEnemy = enemyCreateReqest.GetNewEnemy();

                goNewEnemy.transform.position += Vector3.right * Random.Range(-4f, 4f);
                // goNewEnemy.transform.Rotate(Vector3.forward * 45f);
            }
        }
    }
}