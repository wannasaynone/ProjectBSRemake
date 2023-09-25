using System.Collections.Generic;

namespace ProjectBS.Combat
{
    public class CombatManager 
    {
        public static System.Action OnCombatStarted;

        private List<CombatActor> player;
        private List<CombatActor> enemy;

        public CombatManager()
        {
        }

        public void StartCombat(List<CombatActor> player, List<CombatActor> enemy)
        {
            this.player = new List<CombatActor>(player);
            this.enemy = new List<CombatActor>(enemy);

            OnCombatStarted?.Invoke();

            //WaitActionRate();
        }

        private void WaitActionRate()
        {
            List<CombatActor> allActor = new List<CombatActor>();
            allActor.AddRange(player);
            allActor.AddRange(enemy);

            new WaitingActionRateState(allActor).Enter(ChangeToUnitTurn);
        }

        private void ChangeToUnitTurn()
        {
            List<CombatActor> allActor = new List<CombatActor>();
            allActor.AddRange(player);
            allActor.AddRange(enemy);

            allActor.Sort((x, y) => y.actionRate.CompareTo(x.actionRate));

            new UnitTurnStartState(allActor[0]).Enter(null);
        }
    }
}