using UnityEngine;

namespace Gun
{
    public sealed class BulletImpl_Flash : AbstractBullet
    {
        /**
         * 移动速度
         */
        private const float MOVE_SPEED = 100f;

        private bool _hasOrignPos = false;

        private Vector3 _orignPos;

        public override void DoFly()
        {
            if (!_hasOrignPos)
            {
                _hasOrignPos = true;
                _orignPos = transform.position;
            }

            gameObject.transform.Translate(Vector3.up * MOVE_SPEED * Time.deltaTime);

            if (gameObject.transform.position.y - _orignPos.y > 20f)
            {
                var pos = gameObject.transform.position;
                pos.Set(pos.x, _orignPos.y + 20f, pos.z);
                gameObject.transform.position = pos;

                Destroy(gameObject, 0.2f);
            }
        }
    }
}
