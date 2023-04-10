
using UnityEngine;

namespace Reward
{
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
            _dirX = Random.Range(0, 2) > 0 ? 1 : -1;
            _dirY = Random.Range(0, 2) > 0 ? 1 : -1;
        }

        private void Update()
        {
            if (transform.position.x < -10)
            {
                // left
                _dirX = 1;
            }
            else
            if (transform.position.x > 10)
            {
                // right
                _dirX = -1;
            }

            if (transform.position.y < -20)
            {
                // up
                _dirY = 1;
            }
            else
            if (transform.position.y > 20)
            {
                // down
                _dirY = -1;
            }

            transform.position += 
                Vector3.right * (MOVE_SPEED * _dirX * Time.deltaTime)
                + Vector3.up * (MOVE_SPEED * _dirY * Time.deltaTime);
        }
    }
}
