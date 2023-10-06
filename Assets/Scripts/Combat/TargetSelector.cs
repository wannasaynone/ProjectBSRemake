using KahaGameCore.Combat;
using System;
using System.Collections.Generic;

namespace ProjectBS.Combat
{
    public class TargetSelector : ITargetSelector
    {
        private UI.SelectTargetMenu selectTargetMenu;

        public TargetSelector(UI.SelectTargetMenu selectTargetMenu)
        {
            this.selectTargetMenu = selectTargetMenu;
        }

        public void SetSelectPool(List<CombatActor> player, List<CombatActor> enemy)
        {

        }

        public void StartSimpleSelect(IActor actor, string[] vars, Action<List<CombatActor>> onSelected)
        {

        }

        public void StartRandomSelect(IActor actor, string[] vars, Action<List<CombatActor>> onSelected)
        {
        }
    }
}