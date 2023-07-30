using UnityEngine;

public class GameStarter : MonoBehaviour
{
    private void Start()
    {
        ProjectBS.Combat.CombatManager combatManager = new ProjectBS.Combat.CombatManager();

        ProjectBS.Combat.CombatActor player_actor1 = new ProjectBS.Combat.CombatActor(new ProjectBS.Combat.CombatActor.StatusInfo { Speed = 300 }) { name = "player_actor1" };
        ProjectBS.Combat.CombatActor player_actor2 = new ProjectBS.Combat.CombatActor(new ProjectBS.Combat.CombatActor.StatusInfo { Speed = 200 }) { name = "player_actor2" };
        ProjectBS.Combat.CombatActor player_actor3 = new ProjectBS.Combat.CombatActor(new ProjectBS.Combat.CombatActor.StatusInfo { Speed = 100 }) { name = "player_actor3" };

        ProjectBS.Combat.CombatActor enemy_actor1 = new ProjectBS.Combat.CombatActor(new ProjectBS.Combat.CombatActor.StatusInfo { Speed = 270 }) { name = "enemy_actor1" };
        ProjectBS.Combat.CombatActor enemy_actor2 = new ProjectBS.Combat.CombatActor(new ProjectBS.Combat.CombatActor.StatusInfo { Speed = 180 }) { name = "enemy_actor2" };
        ProjectBS.Combat.CombatActor enemy_actor3 = new ProjectBS.Combat.CombatActor(new ProjectBS.Combat.CombatActor.StatusInfo { Speed = 90 }) { name = "enemy_actor3" };

        combatManager.StartCombat(new System.Collections.Generic.List<ProjectBS.Combat.CombatActor>
        {
            player_actor1,
            player_actor2,
            player_actor3
        }, new System.Collections.Generic.List<ProjectBS.Combat.CombatActor>
        {
            enemy_actor1,
            enemy_actor2,
            enemy_actor3
        });
    }
}
