using System.Collections.Generic;

namespace ProjectBS.Combat
{
    public class CombatManager 
    {
        private readonly UI.CombatUI combatUI;
        private readonly UI.SelectSkillMenu selectSkillMenu;

        private ITargetSelector targetSelector;
        private List<CombatActor> player;
        private List<CombatActor> enemy;

        public CombatManager(ITargetSelector targetSelector, UI.CombatUI combatUI, UI.SelectSkillMenu selectSkillMenu)
        {
            this.targetSelector = targetSelector;
            this.combatUI = combatUI;
            this.selectSkillMenu = selectSkillMenu;
        }

        public void StartCombat(List<CombatActor> player, List<CombatActor> enemy)
        {
            this.player = new List<CombatActor>(player);
            this.enemy = new List<CombatActor>(enemy);

            targetSelector.SetSelectPool(this.player, this.enemy);

            combatUI.ShowWith(player, enemy);

            WaitActionRate();
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

            new UnitTurnStartState(allActor[0], selectSkillMenu).Enter(null);
        }
    }
}