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

        public void ShowWithPlayerTeam(List<CombatUI.CombatActorUIInfo> players)
        {
            enemyGrid_1.Hide();
            enemyGrid_2.Hide();
            enemyGrid_3.Hide();
            enemyGrid_4.Hide();
            enemyGrid_1.Hide();
            bossGrid_1.Hide();
            bossGrid_3.Hide();
            bossGrid_5.Hide();

            playerGrid.ShowWith(players, true);
            root.SetActive(true);
        }

        public void ShowWithEnemyTeam(List<CombatUI.CombatActorUIInfo> enemies)
        {
            playerGrid.Hide();
            enemyGrid_1.Hide();
            enemyGrid_2.Hide();
            enemyGrid_3.Hide();
            enemyGrid_4.Hide();
            enemyGrid_1.Hide();
            bossGrid_1.Hide();
            bossGrid_3.Hide();
            bossGrid_5.Hide();

            switch (enemies.Count)
            {
                case 1: enemyGrid_1.ShowWith(enemies, false); break;
                case 2: enemyGrid_2.ShowWith(enemies, false); break;
                case 3: enemyGrid_3.ShowWith(enemies, false); break;
                case 4: enemyGrid_4.ShowWith(enemies, false); break;
            }
            root.SetActive(true);
        }

        public void ShowWith(List<CombatUI.CombatActorUIInfo> player, List<CombatUI.CombatActorUIInfo> enemy)
        {
            playerGrid.Hide();
            enemyGrid_1.Hide();
            enemyGrid_2.Hide();
            enemyGrid_3.Hide();
            enemyGrid_4.Hide();
            enemyGrid_1.Hide();
            bossGrid_1.Hide();
            bossGrid_3.Hide();
            bossGrid_5.Hide();

            playerGrid.ShowWith(player, true);

            switch (enemy.Count)
            {
                case 1: enemyGrid_1.ShowWith(enemy, false); break;
                case 2: enemyGrid_2.ShowWith(enemy, false); break;
                case 3: enemyGrid_3.ShowWith(enemy, false); break;
                case 4: enemyGrid_4.ShowWith(enemy, false); break;
            }

            root.SetActive(true);
        }
    }
}