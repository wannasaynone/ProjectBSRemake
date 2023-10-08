namespace ProjectBS.Combat
{
    public class UnitTurnStartState : CombatStateBase
    {
        private readonly CombatActor actor;
        private readonly bool isPlayer;
        private readonly UI.SelectSkillMenu selectSkillMenu;
        private readonly Data.GameStaticDataManager gameStaticDataManager;

        public UnitTurnStartState(CombatActor actor, Data.GameStaticDataManager gameStaticDataManager, bool isPlayer, UI.SelectSkillMenu selectSkillMenu)
        {
            this.actor = actor;
            this.isPlayer = isPlayer;
            this.gameStaticDataManager = gameStaticDataManager;
            this.selectSkillMenu = selectSkillMenu;
            this.selectSkillMenu.OnSkillSelected += SelectSkillMenu_OnSkillSelected;
        }

        private void SelectSkillMenu_OnSkillSelected(int skill)
        {
            actor.UseSkillByIndex(skill, OnSkillUsed);
        }

        private void OnSkillUsed()
        {

        }

        public override void Enter(System.Action onEnded)
        {
            selectSkillMenu.ShowWith(CombatUtility.GetUIInfo(gameStaticDataManager, actor, isPlayer));
        }
    }
}