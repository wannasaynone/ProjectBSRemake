namespace ProjectBS.Combat
{
    public class CombatActor : KahaGameCore.Combat.IValueContainer
    {
        public class StatusInfo
        {
            public int MaxHealth;
            public int Attack;
            public int Defense;
            public int Speed;
            public int Critical;
            public int CriticalDefense;
        }

        public string name;

        public int MaxHealth { get; private set; }
        public int OriginMaxHealth { get; private set; }
        public int Health { get; private set; }
        public int Attack { get; private set; }
        public int OrginAttack { get; private set; }
        public int Defense { get; private set; }
        public int OrginDefense { get; private set; }
        public int Speed { get; private set; }
        public int OrginSpeed { get; private set; }
        public int Critical { get; private set; }
        public int OriginCritical { get; private set; }
        public int CriticalDefense { get; private set; }
        public int OriginCriticalDefense { get; private set; }

        public float actionRate;

        // buff 存公式 改GetTotal

        public CombatActor(StatusInfo valueInfo)
        {
            MaxHealth = OriginMaxHealth = valueInfo.MaxHealth;
            Health = MaxHealth;
            Attack = OrginAttack = valueInfo.Attack;
            Defense = OrginDefense = valueInfo.Defense;
            Speed = OrginSpeed = valueInfo.Speed;
            Critical = OriginCritical = valueInfo.Critical;
            CriticalDefense = OriginCriticalDefense = valueInfo.CriticalDefense;
        }

        public int GetTotal(string tag, bool baseOnly)
        {
            switch(tag.Trim().ToLower())
            {
                case "maxhp":
                case "maxhealth":
                    {
                        if (baseOnly)
                            return OriginMaxHealth;
                        else
                            return MaxHealth;
                    }
                case "hp":
                case "health":
                    {
                        return Health;
                    }
                case "attack":
                    {
                        if (baseOnly)
                            return OrginAttack;
                        else
                            return Attack;
                    }
                case "defense":
                    {
                        if (baseOnly)
                            return OrginDefense;
                        else
                            return Defense;
                    }
                case "speed":
                    {
                        if (baseOnly)
                            return OrginSpeed;
                        else
                            return Speed;
                    }
                case "cri":
                case "critical":
                    {
                        if (baseOnly)
                            return OriginCritical;
                        else
                            return Critical;
                    }
                case "crifdef":
                case "cridefense":
                case "criticaldefense":
                    {
                        if (baseOnly)
                            return OriginCriticalDefense;
                        else
                            return CriticalDefense;
                    }
                default:
                    return 0;
            }
        }

        public void Set(string tag, int newValue)
        {
            switch (tag.Trim().ToLower())
            {
                case "hp": case "health": Health = newValue; break;
                case "attack": Attack = newValue; break;
                case "defense": Defense = newValue; break;
                case "speed": Speed = newValue; break;
                case "cri": case "critical": Critical = newValue; break;
                case "crifdef": case "cridefense": case "criticaldefense":  CriticalDefense = newValue; break;
            }
        }
    }
}