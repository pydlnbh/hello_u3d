using UnityEngine;

namespace Gun
{
    public sealed class BulletImpl_Flash : AbstractBullet
    {
        /**
         * 移动速度
         */
        private const float MOVE_SPEED = 100f;

        /**
         * 是否有原始位置
         */
        private bool _hasOrignPos = false;

        /**
         * 原始位置
         */
        private Vector3 _orignPos;

        /**
         * 杀伤力
         */
        private int _dmg;

        public override void DoFly()
        {
            if (!_hasOrignPos)
            {
                _hasOrignPos = true;
                _orignPos = transform.position;
            }

            gameObject.transform.Translate(Vector3.up * (MOVE_SPEED * Time.deltaTime));

            if (gameObject.transform.position.y - _orignPos.y > 20f)
            {
                var pos = gameObject.transform.position;
                pos.Set(pos.x, _orignPos.y + 20f, pos.z);
                gameObject.transform.position = pos;

                Destroy(gameObject, 0.2f);
                return;
            }
        }

        /// <summary>
        /// 设置杀伤力
        /// </summary>
        /// <param name="val">整数值</param>
        /// <returns>this 指针</returns>
        public BulletImpl_Flash PutDmg(int val)
        {
            _dmg = val;
            return this;
        }

        public override int GetDmg() => _dmg;
    }
}
