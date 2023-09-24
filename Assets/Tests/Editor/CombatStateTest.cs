using KahaGameCore.Combat.Processor.EffectProcessor;
using NUnit.Framework;

namespace ProjectBS.Test
{
    public class CombatStateTest
    {
        [Test]
        public void waiting_action_rate_state()
        {
            Combat.CombatActor player_actor1 = new Combat.CombatActor(new Combat.CombatActor.InitialInfo { Speed = 300 }, new EffectCommandDeserializer(new EffectCommandFactoryContainer())) { name = "player_actor1" };
            Combat.CombatActor player_actor2 = new Combat.CombatActor(new Combat.CombatActor.InitialInfo { Speed = 150 }, new EffectCommandDeserializer(new EffectCommandFactoryContainer())) { name = "player_actor2" };
            Combat.CombatActor player_actor3 = new Combat.CombatActor(new Combat.CombatActor.InitialInfo { Speed = 100 }, new EffectCommandDeserializer(new EffectCommandFactoryContainer())) { name = "player_actor3" };

            Combat.CombatActor enemy_actor1 = new Combat.CombatActor(new Combat.CombatActor.InitialInfo { Speed = 270 }, new EffectCommandDeserializer(new EffectCommandFactoryContainer())) { name = "enemy_actor1" };
            Combat.CombatActor enemy_actor2 = new Combat.CombatActor(new Combat.CombatActor.InitialInfo { Speed = 180 }, new EffectCommandDeserializer(new EffectCommandFactoryContainer())) { name = "enemy_actor2" };
            Combat.CombatActor enemy_actor3 = new Combat.CombatActor(new Combat.CombatActor.InitialInfo { Speed = 90 }, new EffectCommandDeserializer(new EffectCommandFactoryContainer())) { name = "enemy_actor3" };

            Combat.WaitingActionRateState waitingActionRateState = new Combat.WaitingActionRateState(new System.Collections.Generic.List<Combat.CombatActor>
            {
                player_actor1,
                player_actor2,
                player_actor3,
                enemy_actor1,
                enemy_actor2,
                enemy_actor3
            });

            waitingActionRateState.Tick(0.25f);

            Assert.IsTrue(UnityEngine.Mathf.Approximately(player_actor1.actionRate, 1f));
            Assert.IsTrue(UnityEngine.Mathf.Approximately(player_actor2.actionRate, 0.5f));
        }
    }
}