using KahaGameCore.GameData;
using System.Threading.Tasks;

namespace ProjectBS
{
    public static class Main
    {
        private class StaticDataLoader : IGameStaticDataHandler
        {
            public T[] Load<T>() where T : IGameData
            {
                string json = UnityEngine.Resources.Load<UnityEngine.TextAsset>("SkillData").text;
                return JsonFx.Json.JsonReader.Deserialize<T[]>(json);
            }

            public Task<T[]> LoadAsync<T>() where T : IGameData
            {
                throw new System.NotImplementedException();
            }
        }

        public static KahaGameCore.GameData.Implemented.GameStaticDataManager GameStaticDataManager { get; private set; }

        public static void Initial()
        {
            GameStaticDataManager = new KahaGameCore.GameData.Implemented.GameStaticDataManager();
            GameStaticDataManager.Add<Data.SkillData>(new StaticDataLoader());
        }
    }
}