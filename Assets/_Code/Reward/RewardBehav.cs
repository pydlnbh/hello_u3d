using UnityEngine;

namespace Reward
{
    /// <summary>
    /// 奖励行为
    /// </summary>
    public sealed class RewardBehav : MonoBehaviour
    {
        /**
         * 飘动的速度
         */
        private const float MOVE_SPEED = 10f;

        /**
         * 飘动的 X 方向
         */
        private int _dirX;

        /**
         * 飘动的 Y 方向
         */
        private int _dirY;

        /// <summary>
        /// Start
        /// </summary>
        private void Start()
        {
            _dirX = Random.Range(0, 2) > 0 ? +1 : -1;
            _dirY = Random.Range(0, 2) > 0 ? +1 : -1;
        }

        /// <summary>
        /// Update
        /// </summary>
        private void Update()
        {
            if (transform.position.x < -10)
            {
                // 左
                _dirX = +1;
            }
            else
            if (transform.position.x > +10)
            {
                // 右
                _dirX = -1;
            }

            if (transform.position.y > +20)
            {
                // 上
                _dirY = -1;
            }
            else
            if (transform.position.y < -20)
            {
                // 下
                _dirY = +1;
            }

            transform.position +=
                Vector3.right * (_dirX * MOVE_SPEED * Time.deltaTime)
                 + Vector3.up * (_dirY * MOVE_SPEED * Time.deltaTime);
        }
    }
}
