using UnityEngine;

/// <summary>
/// 触屏输入策略, 
/// 适用于手机屏幕和鼠标操作
/// </summary>
public sealed class TouchInputStrategy : AbstractInputStrategy
{
    /**
     * 移动到目标位置
     */
    private Vector3 _toWorldPos = Vector3.zero;

    // @Override
    public override void HandleInput()
    {
        DoMove();

        FighterBehav.TheInstance().DoFire();

        var hasTouched = false;
        var toPos = Vector3.zero;

        if (Input.touchCount > 0)
        {
            hasTouched = true;
            toPos = Input.GetTouch(0).position; // 屏幕位置
        }
        else
        if (Input.GetMouseButton(0)) // 如果是点击了鼠标左键
        {
            hasTouched = true;
            toPos = Input.mousePosition; // 屏幕位置
        }

        if (!hasTouched)
        {
            return;
        }

        var fighterAtPos = FighterBehav.TheInstance().transform.position;
        var screenPoint = Camera.main.WorldToScreenPoint(fighterAtPos);
        toPos.z = screenPoint.z;

        _toWorldPos = Camera.main.ScreenToWorldPoint(toPos);
    }

    /// <summary>
    /// 执行移动到某个位置
    /// </summary>
    public void DoMove()
    {
        if (Vector3.zero == _toWorldPos)
        {
            return;
        }

        // 获取战机的当前位置
        var fromWorldPos = FighterBehav.TheInstance().transform.position;

        if ((_toWorldPos - fromWorldPos).sqrMagnitude <= 0.2f)
        {
            // 如果战机当前位置和目标位置非常接近,
            // 则清理目标位置并退出!
            _toWorldPos = Vector3.zero;
            return;
        }

        var diffPos = _toWorldPos - fromWorldPos;
        var normalDir = diffPos;
        normalDir.Normalize(); // 归一化, 最终得到方向值

        FighterBehav.TheInstance().DoMoveBy(normalDir);
    }
}
