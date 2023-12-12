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

        public void Hide()
        {
            root.SetActive(false);
        }

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

            playerGrid.ShowWith(players);
            playerGrid.PlayAllFrameAnimation();

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
                case 1: enemyGrid_1.ShowWith(enemies); enemyGrid_1.PlayAllFrameAnimation(); break;
                case 2: enemyGrid_2.ShowWith(enemies); enemyGrid_2.PlayAllFrameAnimation(); break;
                case 3: enemyGrid_3.ShowWith(enemies); enemyGrid_3.PlayAllFrameAnimation(); break;
                case 4: enemyGrid_4.ShowWith(enemies); enemyGrid_4.PlayAllFrameAnimation(); break;
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

            playerGrid.ShowWith(player);
            playerGrid.PlayAllFrameAnimation();

            switch (enemy.Count)
            {
                case 1: enemyGrid_1.ShowWith(enemy); enemyGrid_1.PlayAllFrameAnimation(); break;
                case 2: enemyGrid_2.ShowWith(enemy); enemyGrid_2.PlayAllFrameAnimation(); break;
                case 3: enemyGrid_3.ShowWith(enemy); enemyGrid_3.PlayAllFrameAnimation(); break;
                case 4: enemyGrid_4.ShowWith(enemy); enemyGrid_4.PlayAllFrameAnimation(); break;
            }

            root.SetActive(true);
        }
    }
}