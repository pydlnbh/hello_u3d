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
    private AbstractGun _currGun1 = new GunImpl_LitBall();

    /**
     * 子弹实现类2
     */
    private AbstractGun _currGun2 = new GunImpl_Flash();

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
            // _currGun1.level = 3;
            // 开火
            // _currGun1.Fire(transform.position);

            // _currGun1.level = 3;
            // 开火
            _currGun2.Fire(transform.position);
        }

    }
}
