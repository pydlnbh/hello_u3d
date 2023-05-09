using Event;
using Gun;
using System;
using UnityEngine;

public sealed class BossBehav : MonoBehaviour
{

    /// <summary>
    /// Boss 血量
    /// </summary>
    private const int MAX_HP = 100;

    /// <summary>
    /// 当前血量
    /// </summary>
    private int _currHp = MAX_HP;

    /// <summary>
    /// 减血事件句柄
    /// </summary>
    public Action<SubtractHpEvent> SubtractHpEventHandler
    {
        get;
        set;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision == null) 
        {
            return;
        }

        var bulletBehav = collision.gameObject.GetComponent<AbstractBullet>();
        _currHp -= bulletBehav.GetDmg();

        SubtractHpEventHandler?.Invoke(
            new SubtractHpEvent(_currHp, MAX_HP, bulletBehav.GetDmg()));

        if (!gameObject.TryGetComponent<Spine.Unity.SkeletonAnimation>(out var spSkeAnim)
            || null == spSkeAnim)
        {
            return;
        }

        spSkeAnim.AnimationState.ClearTrack(1);
        spSkeAnim.AnimationState.SetAnimation(1, "BeHited", false);
    }
}