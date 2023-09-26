using ProjectBS.Combat;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectBS.UI
{
    public class CombatUI : MonoBehaviour
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

        public void ShowWith(List<CombatActor> player, List<CombatActor> enemy)
        {
            playerGrid.ShowWith(player);

            switch(enemy.Count)
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