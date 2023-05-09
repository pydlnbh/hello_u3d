namespace Event
{
    public sealed class SubtractHpEvent
    {
        public SubtractHpEvent(int currHp, int maxHp, int deltaHp) 
        { 
            CurrHp = currHp;
            MaxHp = maxHp;
            DeltaHp = deltaHp;
        }

        public int CurrHp
        {
            get; 
            private set;
        }

        public int MaxHp
        {
            get;
            private set;
        }

        public int DeltaHp
        {
            get;
            private set;
        }
    }
}
