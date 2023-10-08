using NUnit.Framework;
using KahaGameCore.Combat.Processor.EffectProcessor;
using System;
using System.Collections.Generic;
using KahaGameCore.Combat;
using ProjectBS.Combat;

namespace ProjectBS.Test
{
    public class SkillTest
    {
        private class AddAttackCommandFactory : EffectCommandFactoryBase
        {
            public override EffectCommandBase Create()
            {
                return new AddAttackCommand();
            }
        }

        private class AddAttackCommand : EffectCommandBase
        {
            public override void Process(string[] vars, Action onCompleted, Action onForceQuit)
            {
                processData.caster.Stats.AddBase(vars[0], int.Parse(vars[1]));

                for (int i = 0; i < processData.targets.Count; i++)
                {
                    processData.targets[i].Stats.AddBase(vars[0], int.Parse(vars[1]));
                }

                onCompleted?.Invoke();
            }
        }

        private class TestTargetSelecter : ITargetSelector
        {
            private CombatActor preAddTarget;

            public void SetSelectPool(List<CombatActor> player, List<CombatActor> enemy)
            {
                throw new NotImplementedException();
            }

            public void StartRandomSelect(IActor actor, string[] vars, Action<List<CombatActor>> onSelected)
            {
                throw new NotImplementedException();
            }

            public void StartSimpleSelect(IActor actor, string[] vars, Action<List<CombatActor>> onSelected)
            {
                onSelected?.Invoke(new List<CombatActor> { preAddTarget });
            }

            public void PreAddFakeTarget(CombatActor target)
            {
                preAddTarget = target;
            }
        }

        private class AddTargetCommandFactory : EffectCommandFactoryBase
        {
            private readonly TestTargetSelecter targetSelecter;

            public AddTargetCommandFactory(TestTargetSelecter targetSelecter)
            {
                this.targetSelecter = targetSelecter;
            }

            public override EffectCommandBase Create()
            {
                return new AddTargetCommand(targetSelecter);
            }
        }

        private class AddTargetCommand : EffectCommandBase
        {
            private readonly ITargetSelector targetSelecter;

            public AddTargetCommand(ITargetSelector targetSelecter)
            {
                this.targetSelecter = targetSelecter;
            }

            public override void Process(string[] vars, Action onCompleted, Action onForceQuit)
            {
                targetSelecter.StartSimpleSelect(processData.caster, vars, delegate(List<CombatActor> targets)
                {
                    processData.targets.AddRange(targets);
                    onCompleted?.Invoke();
                });
            }
        }

        [Test]
        public void execute_skill()
        {
            AddAttackCommandFactory addAttackCommandFactory = new AddAttackCommandFactory();

            EffectCommandFactoryContainer effectCommandFactoryContainer = new EffectCommandFactoryContainer();
            effectCommandFactoryContainer.RegisterFactory("AddAttack", addAttackCommandFactory);
            
            EffectCommandDeserializer effectCommandDeserializer = new EffectCommandDeserializer(effectCommandFactoryContainer);

            Combat.CombatActor.InitialInfo statusInfo = new Combat.CombatActor.InitialInfo
            {
                Attack = 1,
                Skills = new List<Data.SkillData> { new Data.SkillData(new Data.SkillData.SkillDataTemplete { Commands = "OnActived { AddAttack(Attack, 1); AddAttack(Attack, 5); }" }) }
            };

            int testNumber = 0;
            Combat.CombatActor combatActor = new Combat.CombatActor(statusInfo, effectCommandDeserializer);
            combatActor.UseSkillByIndex(0, delegate { testNumber = 1; });

            Assert.AreEqual(1, testNumber);
            Assert.AreEqual(7, combatActor.Stats.GetTotal("Attack", false));
        }

        [Test]
        public void add_target()
        {
            TestTargetSelecter targetSelecter = new TestTargetSelecter();

            EffectCommandFactoryContainer effectCommandFactoryContainer = new EffectCommandFactoryContainer();
            AddAttackCommandFactory addAttackCommandFactory = new AddAttackCommandFactory();
            AddTargetCommandFactory addTargetCommandFactory = new AddTargetCommandFactory(targetSelecter);
            effectCommandFactoryContainer.RegisterFactory("AddTarget", addTargetCommandFactory);
            effectCommandFactoryContainer.RegisterFactory("AddAttack", addAttackCommandFactory);

            EffectCommandDeserializer effectCommandDeserializer = new EffectCommandDeserializer(effectCommandFactoryContainer);

            CombatActor.InitialInfo statusInfo = new CombatActor.InitialInfo
            {
                Attack = 1,
                Skills = new List<Data.SkillData> { new Data.SkillData(new Data.SkillData.SkillDataTemplete { Commands = "OnActived { AddTarget(); AddAttack(Attack, 1); }" }) }
            };

            int testNumber = 0;
            CombatActor caster = new CombatActor(statusInfo, effectCommandDeserializer);
            CombatActor target = new CombatActor(statusInfo, effectCommandDeserializer);

            targetSelecter.PreAddFakeTarget(target);

            caster.UseSkillByIndex(0, delegate { testNumber = 1; });

            Assert.AreEqual(1, testNumber);
            Assert.AreEqual(2, caster.Stats.GetTotal("Attack", false));
            Assert.AreEqual(2, target.Stats.GetTotal("Attack", false));
        }

        [Test]
        public void trigger()
        {
            AddAttackCommandFactory testCommandFactory = new AddAttackCommandFactory();

            EffectCommandFactoryContainer effectCommandFactoryContainer = new EffectCommandFactoryContainer();
            effectCommandFactoryContainer.RegisterFactory("Test", testCommandFactory);

            EffectCommandDeserializer effectCommandDeserializer = new EffectCommandDeserializer(effectCommandFactoryContainer);

            Combat.CombatActor.InitialInfo statusInfo = new Combat.CombatActor.InitialInfo
            {
                Attack = 1,
                Skills = new List<Data.SkillData> 
                {
                    new Data.SkillData(new Data.SkillData.SkillDataTemplete { Commands = "OnTriggered { Test(Attack, 1); } OnActived { Test(Attack, 1); }" }),
                    new Data.SkillData(new Data.SkillData.SkillDataTemplete { Commands = "OnTriggered { Test(Attack, 1); }" }),
                    new Data.SkillData(new Data.SkillData.SkillDataTemplete { Commands = "OnTriggered { Test(Attack, 1); }" })
                }
            };

            int testNumber = 0;
            Combat.CombatActor combatActor = new Combat.CombatActor(statusInfo, effectCommandDeserializer);
            combatActor.SkillTrigger.Trigger("OnTriggered", delegate { testNumber++; });

            Assert.AreEqual(1, testNumber);
            Assert.AreEqual(4, combatActor.Stats.GetTotal("Attack", false));

            combatActor.SkillTrigger.Trigger("OnActived", delegate { testNumber++; });

            Assert.AreEqual(2, testNumber);
            Assert.AreEqual(5, combatActor.Stats.GetTotal("Attack", false));
        }
    }
}