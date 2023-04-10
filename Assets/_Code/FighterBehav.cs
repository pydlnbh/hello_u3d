using Gun;
using UnityEngine;

public class FighterBehav : MonoBehaviour
{
    /**
     * 移动速度
     */
    private const float MOVE_SPEED = 20f;

    /**
     * 子弹实现类1
     */
    private AbstractGun _currGun = new GunImpl_LitBall()
    {
        level = 1
    };

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
            // 开火
            _currGun.Fire(transform.position);
        }

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name.StartsWith("Reward_Bullet_1"))
        {
            if (_currGun is GunImpl_LitBall)
            {
                _currGun.level = Mathf.Min(4, ++_currGun.level);
            }
            else
            {
                _currGun = new GunImpl_LitBall()
                {
                    level = 1
                };
            }

            GameObject.Destroy(collision.gameObject);
        }
        else 
        if (collision.gameObject.name.StartsWith("Reward_Bullet_2"))
        {
            if (_currGun is GunImpl_Flash)
            {
                _currGun.level = Mathf.Min(4, ++_currGun.level);
            }
            else
            {
                _currGun = new GunImpl_Flash();
            }
            GameObject.Destroy(collision.gameObject);
        }
    }
}
