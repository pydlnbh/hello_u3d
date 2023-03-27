using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gun
{
    /// <summary>
    /// 抽象的子弹
    /// </summary>
    public abstract class AbstractBullet
    {
        /// <summary>
        /// 让子弹飞一会
        /// </summary>
        public abstract void DoFly();
    }
}
