using UnityEngine;

namespace Gun
{
    /// <summary>
    /// 闪电枪
    /// </summary>
    public sealed class GunImpl_Flash : AbstractGun
    {
        /**
         * 上一次发射子弹的时间, 单位 = 秒
         */
        private float _lastFireTime = -1f;

        // @Override
        public override void Fire(Vector3 atWorldPos)
        {
            if (Time.time - _lastFireTime <= 0.3f)
            {
                return;
            }

            _lastFireTime = Time.time;

            BulletFactory.CreateNewBullet(
                "_Bundle.Out/gun",
                "Assets/_Bundle.Src/gun/Prefab/Bullet_2.prefab"
            ).OnComplete += (req) =>
            {
                var goNewBullet = req.GetBullet();
                goNewBullet.GetComponent<BulletImpl_Flash>().PutDmg(Level * 3); // Dmg = Damage
                goNewBullet.transform.position = atWorldPos;
                goNewBullet.SetActive(true);
            };
        }
    }
}
