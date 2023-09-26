using UnityEngine;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private ProjectBS.UI.UIContainer uiContainer;

    private void Start()
    {
        ProjectBS.Main.Initial(uiContainer, Test_StartCombat);
    }

    private void Test_StartCombat()
    {
        ProjectBS.Main.CombatManager.StartCombat(new System.Collections.Generic.List<ProjectBS.Combat.CombatActor>
        {
            Test_CreateActor()
        }, new System.Collections.Generic.List<ProjectBS.Combat.CombatActor>
        {
            Test_CreateActor()
        });
    }

    private ProjectBS.Combat.CombatActor Test_CreateActor()
    {
        ProjectBS.Data.OwingCharacterData owingCharacterData = new ProjectBS.Data.OwingCharacterData
        {
            SourceID = 0,
            Skill_1 = 1
        };

        return ProjectBS.Combat.CombatUtility.ConvertCharacterToCombatActor(owingCharacterData);
    }
}
