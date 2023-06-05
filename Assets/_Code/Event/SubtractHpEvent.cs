namespace Event
{
    /// <summary>
    /// 减血事件
    /// </summary>
    public sealed class SubtractHpEvent
    {
        /// <summary>
        /// 类参数构造器
        /// </summary>
        /// <param name="currHp">当前血量</param>
        /// <param name="maxHp">最大血量</param>
        /// <param name="deltaHp">变化血量</param>
        public SubtractHpEvent(int currHp, int maxHp, int deltaHp)
        {
            CurrHp = currHp;
            MaxHp = maxHp;
            DeltaHp = deltaHp;
        }

        /// <summary>
        /// 当前血量
        /// </summary>
        public int CurrHp
        {
            get;
            private set;
        }

        /// <summary>
        /// 最大血量
        /// </summary>
        public int MaxHp
        {
            get;
            private set;
        }

        /// <summary>
        /// 变化血量
        /// </summary>
        public int DeltaHp
        {
            get;
            private set;
        }
    }
}
