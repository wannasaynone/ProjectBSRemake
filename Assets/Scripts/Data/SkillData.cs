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

        public SkillData() { }

        public struct SkillDataTemplete
        {
            public int ID;
            public int NameID;
            public int DescriptionID;
            public string Commands;
            public string Tags;
        }

        public SkillData(SkillDataTemplete templete)
        {
            ID = templete.ID;
            NameID = templete.NameID;
            DescriptionID = templete.DescriptionID;
            Commands = templete.Commands;
            Tags = templete.Tags;
        }
    }
}