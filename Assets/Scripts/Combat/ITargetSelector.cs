using System.Collections.Generic;

namespace ProjectBS.Combat
{
    public interface ITargetSelector
    {
        public void SetSelectPool(List<CombatActor> player, List<CombatActor> enemy);
        public void StartSimpleSelect(KahaGameCore.Combat.IActor actor, string[] vars, System.Action<List<CombatActor>> onSelected);
        public void StartRandomSelect(KahaGameCore.Combat.IActor actor, string[] vars, System.Action<List<CombatActor>> onSelected);
    }
}