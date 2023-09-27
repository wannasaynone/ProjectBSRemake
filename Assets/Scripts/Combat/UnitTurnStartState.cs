namespace ProjectBS.Combat
{
    public class UnitTurnStartState : CombatStateBase
    {
        private readonly CombatActor actor;
        private readonly UI.SelectSkillMenu selectSkillMenu;

        public UnitTurnStartState(CombatActor actor, UI.SelectSkillMenu selectSkillMenu)
        {
            this.actor = actor;
            this.selectSkillMenu = selectSkillMenu;
            this.selectSkillMenu.OnSkillSelected += SelectSkillMenu_OnSkillSelected;
        }

        private void SelectSkillMenu_OnSkillSelected(Data.SkillData skill)
        {
            
        }

        public override void Enter(System.Action onEnded)
        {
            selectSkillMenu.ShowWith(actor);
        }
    }
}