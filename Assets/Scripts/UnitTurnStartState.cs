using UnityEngine;

namespace ProjectBS.Combat
{
    public class UnitTurnStartState : CombatStateBase
    {
        private CombatActor actor;

        public UnitTurnStartState(CombatActor actor)
        {
            this.actor = actor;
        }

        public override void Enter()
        {
            Debug.Log(actor.name + " turn start");
        }
    }
}