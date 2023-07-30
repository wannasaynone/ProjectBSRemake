using NUnit.Framework;

namespace ProjectBS.Test
{
    public class CombatStateTest
    {
        [Test]
        public void waiting_action_rate_state()
        {
            Combat.CombatActor player_actor1 = new Combat.CombatActor(new Combat.CombatActor.StatusInfo { Speed = 300 }) { name = "player_actor1" };
            Combat.CombatActor player_actor2 = new Combat.CombatActor(new Combat.CombatActor.StatusInfo { Speed = 150 }) { name = "player_actor2" };
            Combat.CombatActor player_actor3 = new Combat.CombatActor(new Combat.CombatActor.StatusInfo { Speed = 100 }) { name = "player_actor3" };

            Combat.CombatActor enemy_actor1 = new Combat.CombatActor(new Combat.CombatActor.StatusInfo { Speed = 270 }) { name = "enemy_actor1" };
            Combat.CombatActor enemy_actor2 = new Combat.CombatActor(new Combat.CombatActor.StatusInfo { Speed = 180 }) { name = "enemy_actor2" };
            Combat.CombatActor enemy_actor3 = new Combat.CombatActor(new Combat.CombatActor.StatusInfo { Speed = 90 }) { name = "enemy_actor3" };

            Combat.WaitingActionRateState waitingActionRateState = new Combat.WaitingActionRateState(new System.Collections.Generic.List<Combat.CombatActor>
            {
                player_actor1,
                player_actor2,
                player_actor3,
                enemy_actor1,
                enemy_actor2,
                enemy_actor3
            });

            Combat.CombatActor startTurnActor = null;
            Combat.WaitingActionRateState.OnActionRateFull += delegate (Combat.CombatActor actor)
            {
                startTurnActor = actor;
            };
            waitingActionRateState.Tick(0.5f);

            Assert.IsTrue(UnityEngine.Mathf.Approximately(player_actor1.actionRate, 1f));
            Assert.IsTrue(UnityEngine.Mathf.Approximately(player_actor2.actionRate, 0.5f));
            Assert.IsNotNull(startTurnActor);
        }
    }
}