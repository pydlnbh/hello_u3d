using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public int level
        {
            get;
            set;
        }

        /// <summary>
        /// 开火
        /// </summary>
        public abstract void Fire();
    }
}
