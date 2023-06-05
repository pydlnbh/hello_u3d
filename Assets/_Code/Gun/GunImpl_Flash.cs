using UnityEngine;

namespace Gun
{
    public sealed class GunImpl_Flash : AbstractGun
    {

        /**
        * 上一次发射子弹的时间, 单位 = 秒
        */
        private float _lastTimes = -1f;

        public override void Fire(Vector3 atWordPos)
        {
            // 如果当前时间减去上一次发射子弹的时间，不执行下面逻辑
            if (Time.time - _lastTimes <= 0.3f)
            {
                return;
            }

            // 把当前时间赋值上去
            _lastTimes = Time.time;

            var req = BulletFactory.createNewBullet(
                "_Bundle.Out/gun",
                "Assets/_Bundle.Src/gun/Prefab/Bullet_2.prefab"
            );

            req.completed += (req) =>
            {
                var newBullet = req.GetBullet();
                newBullet.GetComponent<BulletImpl_Flash>().PutDmg(level * 3);
                newBullet.gameObject.transform.position = atWordPos;
                newBullet.SetActive(true);
            };
        }
    }
}
