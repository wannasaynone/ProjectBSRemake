using UnityEngine;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private TextAsset skillData;

    public static KahaGameCore.GameData.Implemented.GameStaticDataManager GameStaticDataManager { get; private set; }

    private void Start()
    {
        GameStaticDataManager = new KahaGameCore.GameData.Implemented.GameStaticDataManager();

        GameStaticDataManager.Add<ProjectBS.Data.SkillData>(new KahaGameCore.GameData.Implemented.GameStaticDataDeserializer().Read<ProjectBS.Data.SkillData[]>(skillData.text));

        Debug.Log(GameStaticDataManager.GetGameData<ProjectBS.Data.SkillData>(1).Commands);
    }
}
