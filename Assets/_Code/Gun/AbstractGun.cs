using UnityEngine;

namespace Gun
{
    /// <summary>
    /// 抽象的枪
    /// </summary>
    public abstract class AbstractGun
    {
        /// <summary>
        /// 等级
        /// </summary>
        public int Level
        {
            get;
            set;
        }

        /// <summary>
        /// 开火
        /// </summary>
        /// <param name="atWorldPos">所在世界位置</param>
        public abstract void Fire(Vector3 atWorldPos);
    }
}
