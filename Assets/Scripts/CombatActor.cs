using System.Collections.Generic;

namespace ProjectBS.Combat
{
    public class CombatActor : KahaGameCore.Combat.IValueContainer
    {
        public class BuffInfo
        {
            public class InitialInfo
            {
                public CombatActor caster;
                public CombatActor owner;
                public int sourceID;
                public int remainingTurnCount;
                public CombatActor from;
                public string addStatusType;
                public string addValueFormula;
                public List<string> immuneToCommand = new List<string>();
                public List<string> immuneToTag = new List<string>();
            }

            public readonly CombatActor caster;
            public readonly CombatActor owner;
            public readonly int sourceID;
            public readonly int remainingTurnCount;
            public readonly CombatActor from;
            public readonly string addStatusType;
            public readonly string addValueFormula;
            private readonly List<string> immuneToCommand;
            private readonly List<string> immuneToTag;

            public BuffInfo(InitialInfo info)
            {
                caster = info.caster;
                owner = info.owner;
                sourceID = info.sourceID;
                remainingTurnCount = info.remainingTurnCount;
                from = info.from;
                addStatusType = info.addStatusType;
                addValueFormula = info.addValueFormula;
                immuneToCommand = new List<string>(info.immuneToCommand);
                immuneToTag = new List<string>(info.immuneToTag);
            }

            public int GetValue(string tag)
            {
                if (tag != addStatusType)
                    return 0;

                float v = KahaGameCore.Combat.Calculator.Calculate(new KahaGameCore.Combat.Calculator.CalculateData
                {
                    caster = caster,
                    formula = addValueFormula,
                    target = owner,
                    useBaseValue = false
                });

                return System.Convert.ToInt32(v);
            }
        }

        public class StatusInfo
        {
            public int MaxHealth;
            public int Attack;
            public int Defense;
            public int Speed;
            public int Critical;
            public int CriticalDefense;
            public List<int> Skills;
        }

        public class SkillInfo
        {
            public int sourceID;
        }

        public string name;

        public int OriginMaxHealth { get; private set; }
        public int Health { get; private set; }
        public int OrginAttack { get; private set; }
        public int OrginDefense { get; private set; }
        public int OrginSpeed { get; private set; }
        public int OriginCritical { get; private set; }
        public int OriginCriticalDefense { get; private set; }

        public float actionRate;


        private readonly List<SkillInfo> skills;
        private readonly List<BuffInfo> buffs = new List<BuffInfo>();

        public CombatActor(StatusInfo valueInfo)
        {
            OriginMaxHealth = valueInfo.MaxHealth;
            Health = OriginMaxHealth;
            OrginAttack = valueInfo.Attack;
            OrginDefense = valueInfo.Defense;
            OrginSpeed = valueInfo.Speed;
            OriginCritical = valueInfo.Critical;
            OriginCriticalDefense = valueInfo.CriticalDefense;

            skills = new List<SkillInfo>();
            if (valueInfo.Skills != null)
            {
                for (int i = 0; i < valueInfo.Skills.Count; i++)
                {
                    skills.Add(new SkillInfo
                    {
                        sourceID = valueInfo.Skills[i]
                    });
                }
            }
        }

        public int GetTotal(string tag, bool baseOnly)
        {
            tag = tag.Trim().ToLower();
            switch (tag)
            {
                case "maxhp":
                case "maxhealth":
                    {
                        if (baseOnly)
                            return OriginMaxHealth;
                        else
                            return OriginMaxHealth + GetBuffTotal(tag);
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
                            return OrginAttack + GetBuffTotal(tag);
                    }
                case "defense":
                    {
                        if (baseOnly)
                            return OrginDefense;
                        else
                            return OrginDefense + GetBuffTotal(tag);
                    }
                case "speed":
                    {
                        if (baseOnly)
                            return OrginSpeed;
                        else
                            return OrginSpeed + GetBuffTotal(tag);
                    }
                case "cri":
                case "critical":
                    {
                        if (baseOnly)
                            return OriginCritical;
                        else
                            return OriginCritical + GetBuffTotal(tag);
                    }
                case "crifdef":
                case "cridefense":
                case "criticaldefense":
                    {
                        if (baseOnly)
                            return OriginCriticalDefense;
                        else
                            return OriginCriticalDefense + GetBuffTotal(tag);
                    }
                default:
                    return 0;
            }
        }

        private int GetBuffTotal(string valueTag)
        {
            int add = 0;
            for (int i = 0; i < buffs.Count; i++)
            {
                add += buffs[i].GetValue(valueTag);
            }

            return add;
        }
    }
}