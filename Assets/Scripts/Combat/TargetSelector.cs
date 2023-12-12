using KahaGameCore.Combat;
using ProjectBS.UI;
using System;
using System.Collections.Generic;

namespace ProjectBS.Combat
{
    public class TargetSelector : ITargetSelector
    {
        private readonly Data.GameStaticDataManager gameStaticDataManager;
        private readonly SelectTargetMenu selectTargetMenu;

        private List<CombatActor> player;
        private List<CombatActor> enemy;

        private int requireAmount;
        private List<CombatActor> selectedTargets;
        private Action<List<CombatActor>> onSelected;

        public TargetSelector(Data.GameStaticDataManager gameStaticDataManager, SelectTargetMenu selectTargetMenu)
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
            CombatActorCard.OnCardClicked += CombatActorCard_OnCardClicked;

            this.onSelected = onSelected;
            selectedTargets = new List<CombatActor>();

            requireAmount = int.Parse(vars[1]);

            switch (vars[0])
            {
                case "Opponent":
                    {
                        selectTargetMenu.ShowWithEnemyTeam(CombatUtility.GetUIInfo(gameStaticDataManager, enemy, false));
                        break;
                    }
                case "Ally":
                    {
                        selectTargetMenu.ShowWithPlayerTeam(CombatUtility.GetUIInfo(gameStaticDataManager, player, false));
                        break;
                    }
                case "All":
                    {
                        selectTargetMenu.ShowWith(CombatUtility.GetUIInfo(gameStaticDataManager, player, true), CombatUtility.GetUIInfo(gameStaticDataManager, enemy, false));
                        break;
                    }
                case "OtherAlly":
                    {
                        UnityEngine.Debug.Log(vars[0] + " " + vars[1]);
                        break;
                    }
                case "OtherAll":
                    {
                        UnityEngine.Debug.Log(vars[0] + " " + vars[1]);
                        break;
                    }
                default:
                    {
                        UnityEngine.Debug.Log("invaild select range: " + vars[0]);
                        break;
                    }
            }
        }

        private void CombatActorCard_OnCardClicked(int referenceCombatActorHashcode)
        {
            CombatActor selected = player.Find(x => x.GetHashCode() == referenceCombatActorHashcode);
            if (selected == null)
                selected = enemy.Find(x => x.GetHashCode() == referenceCombatActorHashcode);

            if (selected == null)
                return;

            selectedTargets.Add(selected);

            if (selectedTargets.Count >= requireAmount)
            {
                CombatActorCard.OnCardClicked -= CombatActorCard_OnCardClicked;
                selectTargetMenu.Hide();
                onSelected?.Invoke(selectedTargets);
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