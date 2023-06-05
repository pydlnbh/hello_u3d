using UnityEngine;

public sealed class KeyboardInputStrategy : AbstractInputStrategy
{
    public override void HandleInput()
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

        var normalDir = new Vector3(dirX, dirY);
        normalDir.Normalize();

        FighterBehav.TheInstance().DoMoveBy(normalDir);

        if (Input.GetKey(KeyCode.J))
        {
            // 开火
            FighterBehav.TheInstance().DoFire();
        }
    }
}
