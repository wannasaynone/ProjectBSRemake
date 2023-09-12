using UnityEngine;

public class GameStarter : MonoBehaviour
{
    private void Start()
    {
        ProjectBS.Main.Initial(Test_StartCombat);
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
        return new ProjectBS.Combat.CombatActor(new ProjectBS.Combat.CombatActor.InitialInfo
        {
            Attack = Random.Range(100, 200),
            Critical = Random.Range(100, 200),
            CriticalDefense = Random.Range(100, 200),
            Defense = Random.Range(100, 200),
            MaxHealth = Random.Range(100, 200),
            Skills = new System.Collections.Generic.List<ProjectBS.Data.SkillData> { ProjectBS.Main.GameStaticDataManager.GetGameData<ProjectBS.Data.SkillData>(1) },
            Speed = Random.Range(100, 200)
        }, new KahaGameCore.Combat.Processor.EffectProcessor.EffectCommandDeserializer(ProjectBS.Main.EffectCommandFactoryContainer));
    }
}
