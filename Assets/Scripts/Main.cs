using KahaGameCore.Combat.Processor.EffectProcessor;
using KahaGameCore.GameData;
using System;
using System.Threading.Tasks;

namespace ProjectBS
{
    public static class Main
    {
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

            public Task<T[]> LoadAsync<T>() where T : IGameData
            {
                throw new NotImplementedException();
            }
        }

        public static KahaGameCore.GameData.Implemented.GameStaticDataManager GameStaticDataManager { get; private set; }
        public static Combat.CombatManager CombatManager { get; private set; }
        public static EffectCommandFactoryContainer EffectCommandFactoryContainer { get; private set; }
        public static EffectCommandDeserializer EffectCommandDeserializer { get; private set; }

        public static void Initial(Action onLoaded)
        {
            GameStaticDataManager = new KahaGameCore.GameData.Implemented.GameStaticDataManager();
            GameStaticDataManager.Add<Data.SkillData>(new StaticDataLoader("SkillData"));
            GameStaticDataManager.Add<Data.ContextData>(new StaticDataLoader("ContextData"));
            GameStaticDataManager.Add<Data.CharacterData>(new StaticDataLoader("CharacterData"));

            CombatManager = new Combat.CombatManager();

            EffectCommandFactoryContainer = new EffectCommandFactoryContainer();
            EffectCommandFactoryContainer.RegisterFactory("CannotAct", new Combat.Command.EffectCommandFactory_CannotAct());
            EffectCommandFactoryContainer.RegisterFactory("ResetCannotAct", new Combat.Command.EffectCommandFactory_ResetCannotAct());
            EffectCommandFactoryContainer.RegisterFactory("SelectSelf", new Combat.Command.EffectCommandFactory_SelectSelf());
            EffectCommandFactoryContainer.RegisterFactory("RecoverHealth", new Combat.Command.EffectCommandFactory_RecoverHealth());
            EffectCommandFactoryContainer.RegisterFactory("Select", new Combat.Command.EffectCommandFactory_Select());
            EffectCommandFactoryContainer.RegisterFactory("Attack", new Combat.Command.EffectCommandFactory_Attack());
            EffectCommandFactoryContainer.RegisterFactory("PlayAnimation", new Combat.Command.EffectCommandFactory_PlayAnimation());

            EffectCommandDeserializer = new EffectCommandDeserializer(EffectCommandFactoryContainer);

            onLoaded?.Invoke();
        }

        public class TestEffectCommand : EffectCommandBase
        {
            public override void Process(string[] vars, Action onCompleted, Action onForceQuit)
            {
                UnityEngine.Debug.Log("test command");
            }
        }

        public class TestEffectCommandFactory : EffectCommandFactoryBase
        {
            public override EffectCommandBase Create()
            {
                return new TestEffectCommand();
            }
        }
    }
}