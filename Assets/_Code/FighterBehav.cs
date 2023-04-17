using Gun;
using System.Runtime.CompilerServices;
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
        var dirX = 0;
        var dirY = 0;

        if (Input.GetKey(KeyCode.W))
        {
            dirY = 1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            dirX = -1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            dirY = -1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            dirX = 1;
        }

        DoMoveX(dirX);
        DoMoveY(dirY);

        if (Input.GetKey(KeyCode.J))
        {
            // 开火
            _currGun.Fire(transform.position);
        }

    }

    /// <summary>
    /// 向左右移动
    /// </summary>
    /// <param name="dirX">X 轴方向, 取 -1 / +1</param>
    private void DoMoveX(int dirX)
    {
        if (dirX == 0)
        {
            return;
        }

        var hited = Physics.Raycast(
            transform.position + Vector3.left * dirX,
            Vector3.right * dirX,
            out var hitInfo,
            100f,
            LayerMask.GetMask("VirtualWall")
        );

        if (!hited)
        {
            return;
        }

        // 中心到墙的距离, 实际可以移动的距离
        var mxD = hitInfo.distance - 1f;
        // 理论上的移动距离
        var mvD = MOVE_SPEED * Time.deltaTime;

        transform.Translate(Vector3.right * dirX * Mathf.Min(mxD, mvD));
    }

    /// <summary>
    /// 向上下移动
    /// </summary>
    /// <param name="dirY">Y 轴方向, 取 -1 / +1</param>
    private void DoMoveY(int dirY)
    {
        if (dirY == 0)
        {
            return;
        }

        var hited = Physics.Raycast(
            transform.position + Vector3.down * dirY,
            Vector3.up * dirY,
            out var hitInfo,
            100f,
            LayerMask.GetMask("VirtualWall")
        );

        if (!hited)
        {
            return;
        }

        // 中心到墙的距离, 实际可以移动的距离
        var mxD = hitInfo.distance - 1f;
        // 理论上的移动距离
        var mvD = MOVE_SPEED * Time.deltaTime;

        transform.Translate(Vector3.up * dirY * Mathf.Min(mxD, mvD));
    }

    /// <summary>
    /// 带有物理效果的碰撞
    /// </summary>
    /// <param name="collision">另一个碰撞体</param>
    private void OnCollisionEnter(Collision collision)
    {
    }

    /// <summary>
    /// 不带物理效果的碰撞
    /// </summary>
    /// <param name="collision">另一个碰撞体</param>
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name.StartsWith("Reward_Bullet_1"))
        {
            if (_currGun is GunImpl_LitBall)
            {
                _currGun.level = Mathf.Min(3, ++_currGun.level);
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
                _currGun.level = Mathf.Min(3, ++_currGun.level);
            }
            else
            {
                _currGun = new GunImpl_Flash()
                { 
                    level = 2
                };
            }
            GameObject.Destroy(collision.gameObject);
        }
    }
}
