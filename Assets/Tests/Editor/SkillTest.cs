using NUnit.Framework;
using KahaGameCore.Combat.Processor.EffectProcessor;
using System;

namespace ProjectBS.Test
{
    public class SkillTest
    {
        private class TestCommandFactory : EffectCommandFactoryBase
        {
            public override EffectCommandBase Create()
            {
                return new TestAddAttackCommand();
            }
        }

        private class TestAddAttackCommand : EffectCommandBase
        {
            public override void Process(string[] vars, Action onCompleted, Action onForceQuit)
            {
                processData.caster.Add(vars[0], int.Parse(vars[1]));
                onCompleted?.Invoke();
            }
        }

        [Test]
        public void execute_skill()
        {
            TestCommandFactory testCommandFactory = new TestCommandFactory();

            EffectCommandFactoryContainer effectCommandFactoryContainer = new EffectCommandFactoryContainer();
            effectCommandFactoryContainer.RegisterFactory("Test", testCommandFactory);
            
            EffectCommandDeserializer effectCommandDeserializer = new EffectCommandDeserializer(effectCommandFactoryContainer);

            Combat.CombatActor.InitialInfo statusInfo = new Combat.CombatActor.InitialInfo
            {
                Attack = 1,
                Skills = new System.Collections.Generic.List<Data.SkillData> { new Data.SkillData(new Data.SkillData.SkillDataTemplete { Commands = "OnActived { Test(Attack, 1); Test(Attack, 5); }" }) }
            };

            int testNumber = 0;
            Combat.CombatActor combatActor = new Combat.CombatActor(statusInfo, effectCommandDeserializer);
            combatActor.UseSkill(0, delegate { testNumber = 1; });

            Assert.AreEqual(1, testNumber);
            Assert.AreEqual(combatActor.GetTotal("Attack", false), 7);
        }

        [Test]
        public void trigger()
        {
            TestCommandFactory testCommandFactory = new TestCommandFactory();

            EffectCommandFactoryContainer effectCommandFactoryContainer = new EffectCommandFactoryContainer();
            effectCommandFactoryContainer.RegisterFactory("Test", testCommandFactory);

            EffectCommandDeserializer effectCommandDeserializer = new EffectCommandDeserializer(effectCommandFactoryContainer);

            Combat.CombatActor.InitialInfo statusInfo = new Combat.CombatActor.InitialInfo
            {
                Attack = 1,
                Skills = new System.Collections.Generic.List<Data.SkillData> 
                {
                    new Data.SkillData(new Data.SkillData.SkillDataTemplete { Commands = "OnTriggered { Test(Attack, 1); }; OnActived { Test(Attack, 1);" }),
                    new Data.SkillData(new Data.SkillData.SkillDataTemplete { Commands = "OnTriggered { Test(Attack, 1); }" }),
                    new Data.SkillData(new Data.SkillData.SkillDataTemplete { Commands = "OnTriggered { Test(Attack, 1); }" })
                }
            };

            int testNumber = 0;
            Combat.CombatActor combatActor = new Combat.CombatActor(statusInfo, effectCommandDeserializer);
            combatActor.Trigger("OnTriggered", delegate { testNumber++; });

            Assert.AreEqual(1, testNumber);
            Assert.AreEqual(combatActor.GetTotal("Attack", false), 4);
        }
    }
}