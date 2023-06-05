using Event;

using Gun;

using System;

using UnityEngine;

/// <summary>
/// BOSS 行为
/// </summary>
public sealed class BossBehav : MonoBehaviour
{
    /**
     * 最大血量
     */
    private const int MAX_HP = 100;

    /**
     * 当前血量
     */
    private int _currHp = MAX_HP;

    /// <summary>
    /// 减血事件句柄
    /// </summary>
    public Action<SubtractHpEvent> SubtractHpEventHandler
    {
        get;
        set;
    }

    /// <summary>
    /// 当碰撞到其他物体时触发
    /// </summary>
    /// <param name="c">另外一个物体的碰撞体</param>
    private void OnCollisionEnter(Collision c)
    {
        if (null == c)
        {
            return;
        }

        var bulletBehav = c.gameObject.GetComponent<AbstractBullet>();
        _currHp -= bulletBehav.GetDmg();

        SubtractHpEventHandler?.Invoke(
            new SubtractHpEvent(_currHp, MAX_HP, bulletBehav.GetDmg())
        );

        if (!gameObject.TryGetComponent<Spine.Unity.SkeletonAnimation>(out var spSkeAnim) 
         || null == spSkeAnim)
        {
            // 如果没有拿到 Spine 的动画组件,
            // 直接退出!
            return;
        }

        spSkeAnim.AnimationState.ClearTrack(1);
        spSkeAnim.AnimationState.SetAnimation(1, "BeHited", false);
    }
}
