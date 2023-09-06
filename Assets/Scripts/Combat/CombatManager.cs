using System.Collections.Generic;

namespace ProjectBS.Combat
{
    public class CombatManager 
    {
        private List<CombatActor> player;
        private List<CombatActor> enemy;

        public CombatManager()
        {
            WaitingActionRateState.OnActionRateFull += ChangeToUnitTurn;
        }

        public void StartCombat(List<CombatActor> player, List<CombatActor> enemy)
        {
            this.player = new List<CombatActor>(player);
            this.enemy = new List<CombatActor>(enemy);

            List<CombatActor> allActor = new List<CombatActor>();
            allActor.AddRange(player);
            allActor.AddRange(enemy);
            new WaitingActionRateState(allActor).Enter();
        }

        private void ChangeToUnitTurn(CombatActor actor)
        {
            new UnitTurnStartState(actor).Enter();
        }
    }
}