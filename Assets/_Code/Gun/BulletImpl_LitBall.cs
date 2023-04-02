using UnityEngine;

namespace Gun
{
    public sealed class BulletImpl_LitBall : AbstractBullet
    {
        /**
        * ÒÆ¶¯ËÙ¶È
        */
        private const float MOVE_SPEED = 64f;

        public override void DoFly()
        {
            gameObject.transform.Translate(Vector3.up * MOVE_SPEED * Time.deltaTime);

            if (gameObject.transform.position.y > 20f)
            {
                Destroy(gameObject);
            }
        }
    }
}