namespace KahaGameCore.Combat
{
    public class ProcessData 
    {
        public string timing = "";
        public IValueContainer caster = null;
        public System.Collections.Generic.List<IValueContainer> target = null;
        public int skipIfCount = 0;
    }
}