using UnityEngine;

namespace Gun
{
    public sealed class GunImpl_LitBall : AbstractGun
    {

        /**
        * 上一次发射子弹的时间, 单位 = 秒
        */
        private float _lastTimes = -1f;


        public override void Fire(Vector3 atWordPos)
        {

            // 如果当前时间减去上一次发射子弹的时间，不执行下面逻辑
            if (Time.time - _lastTimes <= 0.1f)
            {
                return;
            }

            // 把当前时间赋值上去
            _lastTimes = Time.time;

            var req = BulletFactory.createNewBullet(
                "_Bundle.Out/gun",
                "Assets/_Bundle.src/gun/Prefab/Bullet_1.prefab"
            );

            req.completed += (req) =>
            {
                if (1 == level) 
                {
                    var newBullet = req.GetBullet();
                    newBullet.gameObject.transform.position = atWordPos;
                }
                else
                if (2 == level)
                {
                    var newBullet = req.GetBullet();
                    newBullet.gameObject.transform.position = atWordPos + Vector3.left;

                    newBullet = GameObject.Instantiate(newBullet);
                    newBullet.gameObject.transform.position = atWordPos + Vector3.right;
                }
                else
                if (3 == level)
                {
                    var newBullet = req.GetBullet();
                    newBullet.gameObject.transform.position = atWordPos + Vector3.left;

                    newBullet = GameObject.Instantiate(newBullet);
                    newBullet.gameObject.transform.position = atWordPos;

                    newBullet = GameObject.Instantiate(newBullet);
                    newBullet.gameObject.transform.position = atWordPos + Vector3.right;
                }
            };
        }
    }
}
