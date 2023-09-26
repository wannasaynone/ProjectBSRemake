using KahaGameCore.GameData;

namespace ProjectBS.Data
{
    public class CharacterData : IGameData
    {
        public int ID { get; private set; }
        public int NameID { get; private set; }
        public int Attack { get; private set; }
        public int Defense { get; private set; }
        public int Speed { get; private set; }
        public int Critical { get; private set; }
        public int CriticalDefense { get; private set; }
        public int Health { get; private set; }
        public int PassiveID { get; private set; }
        public int Skill_1 { get; private set; }
        public int Skill_2 { get; private set; }
        public int Skill_3 { get; private set; }
        public int Skill_4 { get; private set; }
        public string Address { get; private set; }
        public float OffsetX { get; private set; }
        public float OffsetY { get; private set; }
    }
}