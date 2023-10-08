using KahaGameCore.Combat;
using System;
using System.Collections.Generic;

namespace ProjectBS.Combat
{
    public class TargetSelector : ITargetSelector
    {
        private readonly Data.GameStaticDataManager gameStaticDataManager;
        private readonly UI.SelectTargetMenu selectTargetMenu;

        private List<CombatActor> player;
        private List<CombatActor> enemy;

        public TargetSelector(Data.GameStaticDataManager gameStaticDataManager, UI.SelectTargetMenu selectTargetMenu)
        {
            this.gameStaticDataManager = gameStaticDataManager;
            this.selectTargetMenu = selectTargetMenu;
        }

        public void SetSelectPool(List<CombatActor> player, List<CombatActor> enemy)
        {
            this.player = player;
            this.enemy = enemy;
        }

        public void StartSimpleSelect(IActor actor, string[] vars, Action<List<CombatActor>> onSelected)
        {
            UnityEngine.Debug.Log(vars[0] + " " + vars[1]);
            switch (vars[0])
            {
                case "Opponent":
                    {
                        selectTargetMenu.ShowWithEnemyTeam(CombatUtility.GetUIInfo(gameStaticDataManager, enemy, false));
                        break;
                    }
                case "Ally":
                    {
                        break;
                    }
                case "All":
                    {
                        break;
                    }
                case "OtherAlly":
                    {
                        break;
                    }
                case "OtherAll": 
                    {
                        break;
                    }
                default:
                    {
                        UnityEngine.Debug.Log("invaild select range: " + vars[0]);
                        break;
                    }
            }
        }

        public void StartRandomSelect(IActor actor, string[] vars, Action<List<CombatActor>> onSelected)
        {
            switch (vars[0])
            {
                case "Opponent": break;
                case "Ally": break;
                case "All": break;
                case "OtherAlly": break;
                case "OtherAll": break;
                default:
                    {
                        UnityEngine.Debug.LogError("invaild select range: " + vars[0]);
                        break;
                    }
            }
        }
    }
}