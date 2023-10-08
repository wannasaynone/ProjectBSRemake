using UnityEngine;
using UnityEngine.UI;

namespace ProjectBS.UI
{
    public class SelectSkillMenu : MonoBehaviour
    {
        public event System.Action<int> OnSkillSelected;

        [SerializeField] private GameObject root;
        [SerializeField] private Image characterImage;
        [SerializeField] private SkillButton[] skillButtons;

        private void Awake()
        {
            for (int i = 0; i < skillButtons.Length; i++)
            {
                skillButtons[i].OnPressed += OnSkillButtonPressed;
            }
        }

        private void OnSkillButtonPressed(int skillDataID)
        {
            OnSkillSelected?.Invoke(skillDataID);
        }

        public void ShowWith(CombatUI.CombatActorUIInfo info)
        {
            transform.parent.gameObject.SetActive(true);
            characterImage.transform.localPosition = info.offset;

            for (int i = 0; i < skillButtons.Length; i++)
            {
                if (info.skills != null && i < info.skills.Count)
                    skillButtons[i].SetUp(info.skills[i]);
                else
                    skillButtons[i].SetUp(new SkillButton.SkillButtonInfo { id = -1 });
            }

            GameResource.GameResourceManager.LoadAsset<Sprite>(info.spriteAddress, OnSpriteLoaded);

            root.SetActive(true);
        }

        private void OnSpriteLoaded(Sprite sprite)
        {
            characterImage.sprite = sprite;
            characterImage.SetNativeSize();
        }

        public void Hide()
        {
            root.SetActive(false);
        }
    }
}
