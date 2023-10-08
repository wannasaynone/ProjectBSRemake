using KahaGameCore.GameData;

namespace ProjectBS.Data
{
    public class GameStaticDataManager
    {
        private readonly KahaGameCore.GameData.Implemented.GameStaticDataManager implementedGameStaticDataManager;

        private class StaticDataLoader : IGameStaticDataHandler
        {
            private readonly string fileName;

            public StaticDataLoader(string fileName)
            {
                this.fileName = fileName;
            }

            public T[] Load<T>() where T : IGameData
            {
                string json = UnityEngine.Resources.Load<UnityEngine.TextAsset>(fileName).text;
                return JsonFx.Json.JsonReader.Deserialize<T[]>(json);
            }

            public System.Threading.Tasks.Task<T[]> LoadAsync<T>() where T : IGameData
            {
                throw new System.NotImplementedException();
            }
        }

        public GameStaticDataManager()
        {
            implementedGameStaticDataManager = new KahaGameCore.GameData.Implemented.GameStaticDataManager();
            implementedGameStaticDataManager.Add<SkillData>(new StaticDataLoader("SkillData"));
            implementedGameStaticDataManager.Add<ContextData>(new StaticDataLoader("ContextData"));
            implementedGameStaticDataManager.Add<CharacterData>(new StaticDataLoader("CharacterData"));
        }

        public T GetData<T>(int id) where T : IGameData
        {
            return implementedGameStaticDataManager.GetGameData<T>(id);
        }
    }
}