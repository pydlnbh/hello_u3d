using UnityEngine;

public class FighterBehav : MonoBehaviour
{

    private const float MOVE_SPEED = 20f;

    /**
     * 上一次发射子弹的时间, 单位 = 秒
     */
    private float _lastTimes = -1f;

    /// <summary>
    /// 在第一帧更新之前执行, 而且只执行一次
    /// </summary>
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            gameObject.transform.Translate(Vector3.up * MOVE_SPEED * Time.deltaTime);

            if (gameObject.transform.position.y > 21f)
            {
                gameObject.transform.position = new Vector3(
                    gameObject.transform.position.x,
                    21f,
                    0f
                );
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            gameObject.transform.Translate(Vector3.left * MOVE_SPEED * Time.deltaTime);

            if (gameObject.transform.position.x < -10f)
            {
                gameObject.transform.position = new Vector3(
                    -10f,
                    gameObject.transform.position.y,
                    0f
                );
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            gameObject.transform.Translate(Vector3.down * MOVE_SPEED * Time.deltaTime);

            if (gameObject.transform.position.y < -19f)
            {
                gameObject.transform.position = new Vector3(
                    gameObject.transform.position.x,
                    -19f,
                    0f
                );
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            gameObject.transform.Translate(Vector3.right * MOVE_SPEED * Time.deltaTime);

            if (gameObject.transform.position.x > 10f)
            {
                gameObject.transform.position = new Vector3(
                    10f,
                    gameObject.transform.position.y,
                    0f
                );
            }
        }

        if (Input.GetKey(KeyCode.J)) 
        {
            // 如果当前时间减去上一次发射子弹的时间，不执行下面逻辑
            if (Time.time - _lastTimes <= 0.1f)
            {
                return;
            }

            // 把当前时间赋值上去
            _lastTimes = Time.time;

            // 找到子弹对象
            var goBullet = gameObject.transform.Find("Bullet").gameObject;
            // 生成子弹对象
            var goNewBullet = GameObject.Instantiate(goBullet);

            goNewBullet.transform.position = gameObject.transform.position;
            goNewBullet.SetActive(true);
        }

    }
}
