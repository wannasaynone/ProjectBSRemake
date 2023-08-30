using KahaGameCore.GameData;

namespace ProjectBS.Data
{
    public class SkillData : IGameData
    {
        public int ID { get; private set; }
        public int NameID { get; private set; }
        public int DescriptionID { get; private set; }
        public string Commands { get; private set; }
        public string Tags { get; private set; }
    }
}