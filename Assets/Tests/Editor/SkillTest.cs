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
            }
        }

        [Test]
        public void excute_skill()
        {
            TestCommandFactory testCommandFactory = new TestCommandFactory();

            EffectCommandFactoryContainer effectCommandFactoryContainer = new EffectCommandFactoryContainer();
            effectCommandFactoryContainer.RegisterFactory("Test", testCommandFactory);
            
            EffectCommandDeserializer effectCommandDeserializer = new EffectCommandDeserializer(effectCommandFactoryContainer);

            Combat.CombatActor.InitialStatusInfo statusInfo = new Combat.CombatActor.InitialStatusInfo
            {
                Attack = 1,
                Skills = new System.Collections.Generic.List<int> { 0 }
            };

            Combat.CombatActor combatActor = new Combat.CombatActor(statusInfo, effectCommandDeserializer);
            combatActor.
        }
    }
}