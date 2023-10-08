using UnityEngine;

namespace ProjectBS.Launcher
{
    public class GameLauncher : MonoBehaviour
    {
        [SerializeField] private UIContainer uiContainer;

        private void Awake()
        {
            Combat.CombatManager combatManager = new Combat.CombatManager(uiContainer.Get<UI.CombatUI>(), uiContainer.Get<UI.SelectSkillMenu>(), uiContainer.Get<UI.SelectTargetMenu>());
            Data.OwingCharacterData owingCharacterData = new Data.OwingCharacterData
            {
                SourceID = 0,
                Skill_1 = 1
            };

            combatManager.StartCombat(new System.Collections.Generic.List<Data.OwingCharacterData>
            {
                owingCharacterData
            }, new System.Collections.Generic.List<Data.OwingCharacterData>
            {
                owingCharacterData
            });
        }
    }
}