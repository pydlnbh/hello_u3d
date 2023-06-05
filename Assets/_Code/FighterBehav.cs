using Gun;

using UnityEngine;

/// <summary>
/// 战机行为
/// </summary>
public class FighterBehav : MonoBehaviour
{
    /**
     * 单例对象
     */
    private static FighterBehav INSTANCE;

    /**
     * 移动速度
     */
    private const float MV_SPEED = 30f;

    /**
     * 当前枪
     */
    private AbstractGun _currGun = new GunImpl_LitBall()
    { 
        Level = 1,
    };

    /// <summary>
    /// 获取单例对象
    /// </summary>
    /// <returns>单例对象</returns>
    public static FighterBehav TheInstance() => INSTANCE;

    /// <summary>
    /// 在第一帧更新之前执行, 而且只执行一次
    /// </summary>
    void Start()
    {
        INSTANCE = this;
    }

    /// <summary>
    /// 每一帧都执行
    /// </summary>
    void Update()
    {
    }

    /// <summary>
    /// 执行开火
    /// </summary>
    public void DoFire()
    {
        // 
        // 开火
        // 
        _currGun.Fire(transform.position);
    }

    /// <summary>
    /// 根据方向移动
    /// </summary>
    /// <param name="normalDir">归一化的方向</param>
    public void DoMoveBy(Vector3 normalDir)
    {
        DoMoveByX(normalDir.x);
        DoMoveByY(normalDir.y);
    }

    /// <summary>
    /// 向左右移动
    /// </summary>
    /// <param name="dirX">X 轴方向, 取 [ -1, +1 ] 之间的数</param>
    private void DoMoveByX(float dirX)
    {
        if (0 == dirX)
        {
            return;
        }

        var hited = Physics.Raycast(
            transform.position + Vector3.left * (dirX < 0 ? -1f : +1f),
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
        var mvD = MV_SPEED * Time.deltaTime;

        transform.Translate(Vector3.right * dirX * Mathf.Min(mxD, mvD));
    }

    /// <summary>
    /// 向上下移动
    /// </summary>
    /// <param name="dirY">Y 轴方向, 取 [ -1, +1 ]</param>
    private void DoMoveByY(float dirY)
    {
        if (0 == dirY)
        {
            return;
        }

        var hited = Physics.Raycast(
            transform.position + Vector3.down * (dirY < 0 ? -1 : +1), 
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
        var mvD = MV_SPEED * Time.deltaTime;

        transform.Translate(Vector3.up * dirY * Mathf.Min(mxD, mvD));
    }

    /// <summary>
    /// 带物理效果的碰撞
    /// </summary>
    /// <param name="c">另一个碰撞体</param>
    private void OnCollisionEnter(Collision c)
    {
    }

    /// <summary>
    /// 不带物理效果的碰撞
    /// </summary>
    /// <param name="c">另一个碰撞体</param>
    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.name.StartsWith("Reward_Bullet_1"))
        {
            if (_currGun is GunImpl_LitBall)
            {
                _currGun.Level = Mathf.Min(4, ++_currGun.Level);
            }
            else
            {
                _currGun = new GunImpl_LitBall()
                {
                    Level = 1
                };
            }

            GameObject.Destroy(c.gameObject);
        }
        else
        if (c.gameObject.name.StartsWith("Reward_Bullet_2"))
        {
            if (_currGun is GunImpl_Flash)
            {
                _currGun.Level = Mathf.Min(3, ++_currGun.Level);
            }
            else
            {
                _currGun = new GunImpl_Flash()
                {
                    Level = 1
                };
            }

            GameObject.Destroy(c.gameObject);
        }
    }
}
