using UnityEngine;

namespace Gun
{
    /// <summary>
    /// 子弹行为
    /// </summary>
    public sealed class BulletImpl_LitBall : AbstractBullet
    {
        /**
         * 移动速度
         */
        private const float MV_SPEED = 64f;

        /// <summary>
        /// OnCollisionEnter
        /// </summary>
        /// <param name="c">另外一个碰撞体</param>
        private void OnCollisionEnter(Collision c)
        {
            if (null != c
             && c.gameObject.name.StartsWith("Enemy_"))
            {
                Destroy(gameObject);
            }
        }

        // @Override
        public override void DoFly()
        {
            gameObject.transform.Translate(Vector3.up * (MV_SPEED * Time.deltaTime));

            if (gameObject.transform.position.y > 30f)
            {
                Destroy(gameObject);
            }
        }

        // @Override
        public override int GetDmg() => 1;
    }
}
