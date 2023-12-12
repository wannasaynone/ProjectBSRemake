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

        public class CombatActorUIInfo
        {
            public string spriteAddress;
            public bool isPlayer;
            public Vector3 offset;
            public List<SkillButton.SkillButtonInfo> skills;
            public int referenceCombatActorHashcode;
            public int attack;
            public int defense;
            public int hp;
            public int sp;
        }

        public void ShowWith(List<CombatActorUIInfo> player, List<CombatActorUIInfo> enemy)
        {
            enemyGrid_1.Hide();
            enemyGrid_2.Hide();
            enemyGrid_3.Hide();
            enemyGrid_4.Hide();
            bossGrid_1.Hide();
            bossGrid_3.Hide();
            bossGrid_5.Hide();

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