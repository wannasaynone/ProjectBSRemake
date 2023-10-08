using System.Collections.Generic;
using KahaGameCore.Combat.Processor.EffectProcessor;

namespace ProjectBS.Combat
{
    public class CombatManager 
    {
        private readonly UI.CombatUI combatUI;
        private readonly UI.SelectSkillMenu selectSkillMenu;
        private readonly TargetSelector targetSelector;
        private readonly Data.GameStaticDataManager gameStaticDataManager;
        private readonly EffectCommandDeserializer effectCommandDeserializer;

        private List<CombatActor> player;
        private List<CombatActor> enemy;

        public CombatManager(UI.CombatUI combatUI,
                             UI.SelectSkillMenu selectSkillMenu,
                             UI.SelectTargetMenu selectTargetMenu)
        {
            this.combatUI = combatUI;
            this.selectSkillMenu = selectSkillMenu;

            gameStaticDataManager = new Data.GameStaticDataManager();

            this.targetSelector = new TargetSelector(gameStaticDataManager, selectTargetMenu);

            EffectCommandFactoryContainer effectCommandFactoryContainer = new EffectCommandFactoryContainer();
            effectCommandFactoryContainer.RegisterFactory("CannotAct", new Command.EffectCommandFactory_CannotAct());
            effectCommandFactoryContainer.RegisterFactory("ResetCannotAct", new Command.EffectCommandFactory_ResetCannotAct());
            effectCommandFactoryContainer.RegisterFactory("SelectSelf", new Command.EffectCommandFactory_SelectSelf());
            effectCommandFactoryContainer.RegisterFactory("RecoverHealth", new Command.EffectCommandFactory_RecoverHealth());
            effectCommandFactoryContainer.RegisterFactory("Select", new Command.EffectCommandFactory_Select(targetSelector));
            effectCommandFactoryContainer.RegisterFactory("Attack", new Command.EffectCommandFactory_Attack());
            effectCommandFactoryContainer.RegisterFactory("PlayAnimation", new Command.EffectCommandFactory_PlayAnimation());

            effectCommandDeserializer = new EffectCommandDeserializer(effectCommandFactoryContainer);
        }

        public void StartCombat(List<Data.OwingCharacterData> player, List<Data.OwingCharacterData> enemy)
        {
            this.player = GetCombatActors(player);
            this.enemy = GetCombatActors(enemy);

            targetSelector.SetSelectPool(this.player, this.enemy);

            combatUI.ShowWith(CombatUtility.GetUIInfo(gameStaticDataManager, this.player, true), CombatUtility.GetUIInfo(gameStaticDataManager, this.enemy, false));

            WaitActionRate();
        }

        private List<CombatActor> GetCombatActors(List<Data.OwingCharacterData> owingCharacters)
        {
            List<CombatActor> combatActors = new List<CombatActor>();
            for (int i = 0; i < owingCharacters.Count; i++)
            {
                combatActors.Add(CombatUtility.ConvertCharacterToCombatActor(effectCommandDeserializer, gameStaticDataManager, owingCharacters[i]));
            }

            return combatActors;
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

            new UnitTurnStartState(allActor[0], gameStaticDataManager, player.Contains(allActor[0]), selectSkillMenu).Enter(null);
        }
    }
}