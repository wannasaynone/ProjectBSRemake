using KahaGameCore.GameData;

namespace ProjectBS.Data
{
    public class ContextData : IGameData
    {
        public int ID { get; private set; }
        public string zh_tw { get; private set; }
        public string en_us { get; private set; }
    }
}