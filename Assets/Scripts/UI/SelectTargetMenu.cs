using UnityEngine;
using System.Collections.Generic;

namespace ProjectBS.UI
{
    public class SelectTargetMenu : MonoBehaviour
    {
        [SerializeField] private GameObject root;
        [SerializeField] private CombatActorGrid playerGrid;
        [SerializeField] private CombatActorGrid enemyGrid_1;
        [SerializeField] private CombatActorGrid enemyGrid_2;
        [SerializeField] private CombatActorGrid enemyGrid_3;
        [SerializeField] private CombatActorGrid enemyGrid_4;
        [SerializeField] private CombatActorGrid bossGrid_1;
        [SerializeField] private CombatActorGrid bossGrid_3;
        [SerializeField] private CombatActorGrid bossGrid_5;

        public void ShowWithPlayerTeam(List<Combat.CombatActor> players)
        {
            playerGrid.ShowWith(players);
            root.SetActive(true);
        }

        public void ShowWithEnemyTeam(List<Combat.CombatActor> enemies)
        {
            switch (enemies.Count)
            {
                case 1: enemyGrid_1.ShowWith(enemies); break;
                case 2: enemyGrid_2.ShowWith(enemies); break;
                case 3: enemyGrid_3.ShowWith(enemies); break;
                case 4: enemyGrid_4.ShowWith(enemies); break;
            }
            root.SetActive(true);
        }

        public void ShowWith(List<Combat.CombatActor> player, List<Combat.CombatActor> enemy)
        {
            playerGrid.ShowWith(player);

            switch (enemy.Count)
            {
                case 1: enemyGrid_1.ShowWith(enemy); break;
                case 2: enemyGrid_2.ShowWith(enemy); break;
                case 3: enemyGrid_3.ShowWith(enemy); break;
                case 4: enemyGrid_4.ShowWith(enemy); break;
            }

            root.SetActive(true);
        }
    }
}