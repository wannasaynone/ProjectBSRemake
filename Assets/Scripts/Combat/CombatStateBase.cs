namespace ProjectBS.Combat
{
    public abstract class CombatStateBase
    {
        public abstract void Enter(System.Action onEnded);
    }
}