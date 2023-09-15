using UnityEngine;

namespace ProjectBS.Combat
{
    public class UnitTurnStartState : CombatStateBase
    {
        public static System.Action<CombatActor> OnTurnStarted;

        private readonly CombatActor actor;

        public UnitTurnStartState(CombatActor actor)
        {
            this.actor = actor;
        }

        public override void Enter()
        {
            OnTurnStarted?.Invoke(actor);
        }
    }
}