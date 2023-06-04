using UnityEngine;

public sealed class TouchInputStrategy : AbstractInputStrategy
{
    /**
     * 移动到目标位置
     */
    private Vector3 _toWorldPos = Vector3.zero;

    public override void HandleInput()
    {
        // 点击移动
        DoMoveTo();

        // 自动开火
        FighterBehav.TheInstance().DoFire();

        var hasTouched = false;
        var toPos = Vector3.zero;

        if (Input.touchCount > 0)
        {
            hasTouched = true;
            toPos = Input.GetTouch(0).position;
        }
        else 
        if (Input.GetMouseButton(0))
        {
            hasTouched = true;
            toPos = Input.mousePosition;
        }

        if (!hasTouched)
        {
            return;
        }

        var fighterPos = FighterBehav.TheInstance().transform.position;
        var screenPoint = Camera.main.WorldToScreenPoint(fighterPos);
        toPos.z = screenPoint.z;

        _toWorldPos = Camera.main.ScreenToWorldPoint(toPos);
    }

    private void DoMoveTo()
    {
        if (Vector3.zero == _toWorldPos)
        {
            return;
        }

        var diffPos = _toWorldPos - FighterBehav.TheInstance().transform.position;

        if (diffPos.sqrMagnitude < 0.2f)
        {
            return;
        }

        var normalDir = diffPos;
        normalDir.Normalize();

        FighterBehav.TheInstance().DoMoveBy(normalDir);
    }
}

