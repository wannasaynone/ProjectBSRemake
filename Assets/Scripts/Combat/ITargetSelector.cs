namespace ProjectBS.Combat
{
    public interface ITargetSelector
    {
        public void StartSelect(string[] vars, System.Action<System.Collections.Generic.List<KahaGameCore.Combat.IActor>> onSelected);
    }
}