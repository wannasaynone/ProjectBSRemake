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
        public int Health { get; private set; }
    }
}