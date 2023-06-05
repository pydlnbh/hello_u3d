using UnityEngine;

namespace Gun
{
    /// <summary>
    /// 光球枪
    /// </summary>
    public sealed class GunImpl_LitBall : AbstractGun
    {
        /**
         * 上一次发射子弹的时间, 单位 = 秒
         */
        private float _lastFireTime = -1f;

        // @Override
        public override void Fire(Vector3 atWorldPos)
        {
            if (Time.time - _lastFireTime <= 0.1f)
            {
                // 不能太连发了,
                // 子弹都连成一条线了...
                return;
            }

            _lastFireTime = Time.time;

            BulletFactory.CreateNewBullet(
                "_Bundle.Out/gun",
                "Assets/_Bundle.Src/gun/Prefab/Bullet_1.prefab"
            ).OnComplete += (req) =>
            {
                if (1 == Level)
                {
                    var goNewBullet = req.GetBullet();

                    goNewBullet.transform.position = atWorldPos;
                    goNewBullet.SetActive(true);
                }
                else
                if (2 == Level)
                {
                    var goNewBullet = req.GetBullet();

                    goNewBullet.transform.position = atWorldPos + Vector3.left;
                    goNewBullet.SetActive(true);

                    goNewBullet = GameObject.Instantiate(goNewBullet);
                    goNewBullet.transform.position = atWorldPos + Vector3.right;
                    goNewBullet.SetActive(true);
                }
                else
                if (3 == Level)
                {
                    var newBullet = req.GetBullet();
                    newBullet.gameObject.transform.position = atWorldPos + Vector3.left;

                    newBullet = GameObject.Instantiate(newBullet);
                    newBullet.gameObject.transform.position = atWorldPos;

                    newBullet = GameObject.Instantiate(newBullet);
                    newBullet.gameObject.transform.position = atWorldPos + Vector3.right;
                }
            };
        }
    }
}
