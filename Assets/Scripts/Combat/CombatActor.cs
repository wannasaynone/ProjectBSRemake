using System.Collections.Generic;
using KahaGameCore.Combat.Processor.EffectProcessor;
using System;

namespace ProjectBS.Combat
{
    public class CombatActor : KahaGameCore.Combat.IActor
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

                return Convert.ToInt32(v);
            }

            public void Execute(string timing)
            {
                throw new System.NotImplementedException();
            }
        }

        public class InitialInfo
        {
            public int sourceID;
            public int MaxHealth;
            public int Attack;
            public int Defense;
            public int Speed;
            public int Critical;
            public int CriticalDefense;
            public int Effectiveness;
            public int Resistance;
            public List<Data.SkillData> Skills;
        }

        public class SkillInfo
        {
            public class ProcessableSkillInfo : KahaGameCore.Processor.IProcessable
            {
                public readonly Action<CombatActor, string, Action> execute;

                private readonly CombatActor caster;
                private readonly string timing;

                public ProcessableSkillInfo(CombatActor caster, string timing, Action<CombatActor, string, Action> execute)
                {
                    this.execute = execute;
                    this.caster = caster;
                    this.timing = timing;
                }

                public void Process(Action onCompleted, Action onForceQuit)
                {
                    execute?.Invoke(caster, timing, onCompleted);
                }
            }

            public readonly Data.SkillData referenceSkillData;
            private readonly EffectProcessor effectProcessor;
            private Action onEnded;

            public SkillInfo(Data.SkillData skillData, EffectCommandDeserializer effectCommandDeserializer)
            {
                referenceSkillData = skillData;
                Dictionary<string, List<EffectProcessor.EffectData>> timingToCommands = effectCommandDeserializer.Deserialize(skillData.Commands);
                effectProcessor = new EffectProcessor();
                effectProcessor.SetUp(timingToCommands);
            }

            public void Execute(CombatActor caster, string timing, Action onEnded)
            {
                if (!effectProcessor.HasTiming(timing))
                {
                    onEnded?.Invoke();
                    return;
                }

                this.onEnded = onEnded;
                effectProcessor.OnProcessEnded += EffectProcessor_OnProcessEnded;
                effectProcessor.Start(new KahaGameCore.Combat.ProcessData
                {
                    caster = caster,
                    timing = timing,
                    skipIfCount = 0
                });
            }

            private void EffectProcessor_OnProcessEnded()
            {
                effectProcessor.OnProcessEnded -= EffectProcessor_OnProcessEnded;
                onEnded?.Invoke();
            }

            public ProcessableSkillInfo GetProcessableSkillInfo(CombatActor caster, string timing)
            {
                return new ProcessableSkillInfo(caster, timing, Execute);
            }
        }

        public string name;

        public class ActorStats : KahaGameCore.Combat.IValueContainer
        {
            private struct ValueContainer
            {
                public Guid guid;
                public string tag;
                public int value;

                public void AddValue(int newValue)
                {
                    value += newValue;
                }

                public void SetValue(int newValue)
                {
                    value = newValue;
                }
            }

            private readonly List<BuffInfo> buffs = new List<BuffInfo>();
            private readonly List<ValueContainer> tempValues = new List<ValueContainer>();

            public int OriginMaxHealth { get; private set; }
            public int Health { get; private set; }
            public int OrginAttack { get; private set; }
            public int OrginDefense { get; private set; }
            public int OrginSpeed { get; private set; }
            public int OriginCritical { get; private set; }
            public int OriginCriticalDefense { get; private set; }
            public int OriginEffectiveness { get; private set; }
            public int OriginResistance { get; private set; }

            public ActorStats(InitialInfo valueInfo)
            {
                OriginMaxHealth = valueInfo.MaxHealth;
                Health = OriginMaxHealth;
                OrginAttack = valueInfo.Attack;
                OrginDefense = valueInfo.Defense;
                OrginSpeed = valueInfo.Speed;
                OriginCritical = valueInfo.Critical;
                OriginCriticalDefense = valueInfo.CriticalDefense;
                OriginEffectiveness = valueInfo.Effectiveness;
                OriginResistance = valueInfo.Resistance;
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
                                return OriginMaxHealth + GetBuffTotal(Const.HP) + GetTempTotal(Const.HP);
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
                                return OrginAttack + GetBuffTotal(Const.Attack) + GetTempTotal(Const.Attack);
                        }
                    case "defense":
                        {
                            if (baseOnly)
                                return OrginDefense;
                            else
                                return OrginDefense + GetBuffTotal(Const.Defense) + GetTempTotal(Const.Defense);
                        }
                    case "speed":
                        {
                            if (baseOnly)
                                return OrginSpeed;
                            else
                                return OrginSpeed + GetBuffTotal(Const.Speed) + GetTempTotal(Const.Speed);
                        }
                    case "cri":
                    case "critical":
                        {
                            if (baseOnly)
                                return OriginCritical;
                            else
                                return OriginCritical + GetBuffTotal(Const.Critical) + GetTempTotal(Const.Critical);
                        }
                    case "crifdef":
                    case "cridefense":
                    case "criticaldefense":
                        {
                            if (baseOnly)
                                return OriginCriticalDefense;
                            else
                                return OriginCriticalDefense + GetBuffTotal(Const.CriticalDefense) + GetTempTotal(Const.CriticalDefense);
                        }
                    case "effect":
                    case "effectiveness":
                        {
                            if (baseOnly)
                                return OriginEffectiveness;
                            else
                                return OriginEffectiveness + GetBuffTotal(Const.Effectiveness) + GetTempTotal(Const.Effectiveness);
                        }
                    case "resistance":
                    case "resist":
                        {
                            if (baseOnly)
                                return OriginResistance;
                            else
                                return OriginResistance + GetBuffTotal(Const.Resistance) + GetTempTotal(Const.Resistance);
                        }
                    default:
                        return GetTempTotal(tag);
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

            private int GetTempTotal(string valueTag)
            {
                int add = 0;
                for (int i = 0; i < tempValues.Count; i++)
                {
                    if (tempValues[i].tag == valueTag)
                    {
                        add += tempValues[i].value;
                    }
                }

                return add;
            }

            public void AddBase(string tag, int value)
            {
                tag = tag.Trim().ToLower();
                switch (tag)
                {
                    case "maxhp":
                    case "maxhealth":
                        {
                            OriginMaxHealth += value;

                            if (OriginMaxHealth < 1)
                            {
                                OriginMaxHealth = 1;
                            }
                            break;
                        }
                    case "hp":
                    case "health":
                        {
                            Health += value;
                            int max = GetTotal("maxhp", false);
                            if (Health > max)
                            {
                                Health = max;
                            }
                            break;
                        }
                    case "attack":
                        {
                            OrginAttack += value;
                            if (OrginAttack < 0) OrginAttack = 0;
                            break;
                        }
                    case "defense":
                        {
                            OrginDefense += value;
                            if (OrginDefense < 0) OrginDefense = 0;
                            break;
                        }
                    case "speed":
                        {
                            OrginSpeed += value;
                            if (OrginSpeed < 0) OrginSpeed = 0;
                            break;
                        }
                    case "cri":
                    case "critical":
                        {
                            OriginCritical += value;
                            if (OriginCritical < 0) OriginCritical = 0;
                            break;
                        }
                    case "crifdef":
                    case "cridefense":
                    case "criticaldefense":
                        {
                            OriginCriticalDefense += value;
                            if (OriginCriticalDefense < 0) OriginCriticalDefense = 0;
                            break;
                        }
                    default:
                        UnityEngine.Debug.LogError("invaild stats tag=" + tag);
                        break;
                }
            }

            public void SetBase(string tag, int value)
            {
                tag = tag.Trim().ToLower();
                switch (tag)
                {
                    case "maxhp":
                    case "maxhealth":
                        {
                            OriginMaxHealth = value;

                            if (OriginMaxHealth < 1)
                            {
                                OriginMaxHealth = 1;
                            }
                            break;
                        }
                    case "hp":
                    case "health":
                        {
                            Health = value;
                            int max = GetTotal("maxhp", false);
                            if (Health > max)
                            {
                                Health = max;
                            }
                            break;
                        }
                    case "attack":
                        {
                            OrginAttack = value;
                            if (OrginAttack < 0) OrginAttack = 0;
                            break;
                        }
                    case "defense":
                        {
                            OrginDefense = value;
                            if (OrginDefense < 0) OrginDefense = 0;
                            break;
                        }
                    case "speed":
                        {
                            OrginSpeed = value;
                            if (OrginSpeed < 0) OrginSpeed = 0;
                            break;
                        }
                    case "cri":
                    case "critical":
                        {
                            OriginCritical = value;
                            if (OriginCritical < 0) OriginCritical = 0;
                            break;
                        }
                    case "crifdef":
                    case "cridefense":
                    case "criticaldefense":
                        {
                            OriginCriticalDefense = value;
                            if (OriginCriticalDefense < 0) OriginCriticalDefense = 0;
                            break;
                        }
                    default:
                        UnityEngine.Debug.LogError("invaild stats tag=" + tag);
                        break;
                }
            }

            public Guid Add(string tag, int value)
            {
                Guid newGuid = Guid.NewGuid();

                tempValues.Add(new ValueContainer
                {
                    guid = newGuid,
                    tag = tag,
                    value = value
                });

                return newGuid;
            }

            public void SetTemp(Guid guid, int value)
            {
                for (int i = 0; i < tempValues.Count; i++)
                {
                    if (tempValues[i].guid.CompareTo(guid) == 0)
                    {
                        tempValues[i].SetValue(value);
                        break;
                    }
                }
            }

            public void AddToTemp(Guid guid, int value)
            {
                for (int i = 0; i < tempValues.Count; i++)
                {
                    if (tempValues[i].guid.CompareTo(guid) == 0)
                    {
                        tempValues[i].AddValue(value);
                        break;
                    }
                }
            }
        }

        public class ActorSkillTrigger : KahaGameCore.Combat.ISkillTrigger
        {
            public int SkillCount { get { return skills.Count; } }

            private readonly CombatActor combatActor;
            private readonly List<SkillInfo> skills;

            public ActorSkillTrigger(CombatActor combatActor, List<SkillInfo> skills)
            {
                this.combatActor = combatActor;
                this.skills = new List<SkillInfo>(skills);
            }

            public void Trigger(string timing, Action onEnded)
            {
                List<SkillInfo.ProcessableSkillInfo> processableSkillInfos = new List<SkillInfo.ProcessableSkillInfo>();
                for (int i = 0; i < skills.Count; i++)
                {
                    processableSkillInfos.Add(skills[i].GetProcessableSkillInfo(combatActor, timing));
                }

                KahaGameCore.Processor.Processor<SkillInfo.ProcessableSkillInfo> processor = new KahaGameCore.Processor.Processor<SkillInfo.ProcessableSkillInfo>(processableSkillInfos.ToArray());
                processor.Start(onEnded, delegate { UnityEngine.Debug.LogError("Trigger shouldn't have Quit"); });
            }

            public void UseSkill(Data.SkillData skillData, Action onUsed)
            {
                SkillInfo skillInfo = skills.Find(x => x.referenceSkillData.ID == skillData.ID);
                if (skillInfo != null)
                {
                    skillInfo.Execute(combatActor, Const.OnActived, onUsed);
                }
                else
                {
                    UnityEngine.Debug.LogError("using skill invaild id=" + skillData.ID);
                    onUsed?.Invoke();
                }
            }

            public void UseSkillByIndex(int index, Action onUsed)
            {
                if (index < 0 || index >= skills.Count)
                {
                    UnityEngine.Debug.LogError("using skill invaild index=" + index);
                    onUsed?.Invoke();
                    return;
                }

                skills[index].Execute(combatActor, Const.OnActived, onUsed);
            }

            public void UseSkillByID(int id, Action onUsed)
            {
                for (int i = 0; i < skills.Count; i++)
                {
                    if (skills[i].referenceSkillData.ID == id)
                    {
                        skills[i].Execute(combatActor, Const.OnActived, onUsed);
                        return;
                    }
                }

                onUsed?.Invoke();
            }

            public Data.SkillData GetSkillSourceByIndex(int index)
            {
                if (index < 0 || index >= skills.Count)
                {
                    return null;
                }

                return skills[index].referenceSkillData;
            }
        }

        public readonly int sourceID;

        public float actionRate;

        public KahaGameCore.Combat.IValueContainer Stats { get; private set; }
        public KahaGameCore.Combat.ISkillTrigger SkillTrigger { get; private set; }

        public CombatActor(InitialInfo valueInfo, EffectCommandDeserializer effectCommandDeserializer)
        {
            sourceID = valueInfo.sourceID;
            Stats = new ActorStats(valueInfo);

            List<SkillInfo> skills = new List<SkillInfo>();
            if (valueInfo.Skills != null)
            {
                for (int i = 0; i < valueInfo.Skills.Count; i++)
                {
                    if (valueInfo.Skills[i].ID <= -1)
                        continue;

                    skills.Add(new SkillInfo(valueInfo.Skills[i], effectCommandDeserializer));
                }
            }

            SkillTrigger = new ActorSkillTrigger(this, skills);
        }

        public void UseSkillByIndex(int index, Action onUsed)
        {
            ((ActorSkillTrigger)SkillTrigger).UseSkillByIndex(index, onUsed);
        }

        public Data.SkillData GetSkillSourceByIndex(int index)
        {
            return ((ActorSkillTrigger)SkillTrigger).GetSkillSourceByIndex(index);
        }

        public int GetSkillCount()
        {
            return ((ActorSkillTrigger)SkillTrigger).SkillCount;
        }
    }
}