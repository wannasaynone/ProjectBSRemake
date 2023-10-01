using ProjectBS.Combat;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace ProjectBS.UI
{
    public class SelectSkillMenu : MonoBehaviour
    {
        public event System.Action<Data.SkillData> OnSkillSelected;

        [SerializeField] private GameObject root;
        [SerializeField] private Image characterImage;
        [SerializeField] private SkillButton[] skillButtons;

        private Data.CharacterData referenceCharacterData;

        private void Awake()
        {
            for (int i = 0; i < skillButtons.Length; i++)
            {
                skillButtons[i].OnPressed += OnSkillButtonPressed;
            }
        }

        private void OnSkillButtonPressed(Data.SkillData skillData)
        {
            OnSkillSelected?.Invoke(skillData);
        }

        public void ShowWith(CombatActor combatActor)
        {
            referenceCharacterData = combatActor.GetSource();

            AsyncOperationHandle downloadAsync = Addressables.LoadAssetAsync<Sprite>(referenceCharacterData.Address);
            downloadAsync.Completed += DownloadAsync_Completed;

            for (int i = 0; i < skillButtons.Length; i++)
            {
                skillButtons[i].SetUp(combatActor.GetSkillSource(i));
            }

            root.SetActive(true);
        }

        public void Hide()
        {
            root.SetActive(false);
        }

        private void DownloadAsync_Completed(AsyncOperationHandle obj)
        {
            transform.parent.gameObject.SetActive(true);
            characterImage.sprite = obj.Result as Sprite;
            characterImage.SetNativeSize();
            characterImage.transform.localPosition = new Vector3(referenceCharacterData.OffsetX, referenceCharacterData.OffsetY);
        }
    }
}
