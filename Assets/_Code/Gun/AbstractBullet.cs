using UnityEngine;

namespace Gun
{
    /// <summary>
    /// 抽象的子弹
    /// </summary>
    public abstract class AbstractBullet : MonoBehaviour
    {

        private void Update()
        {
            DoFly();
        }

        /// <summary>
        /// 让子弹飞一会
        /// </summary>
        public abstract void DoFly();
    }
}
