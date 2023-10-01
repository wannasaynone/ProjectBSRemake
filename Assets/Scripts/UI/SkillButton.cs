using UnityEngine;
using UnityEngine.UI;

namespace ProjectBS.UI
{
    public class SkillButton : MonoBehaviour
    {
        public event System.Action<Data.SkillData> OnPressed;

        [SerializeField] private Text skillNameText;
        [SerializeField] private Button button;

        private Data.SkillData referenceSkillData;

        public void SetUp(Data.SkillData skillData)
        {
            referenceSkillData = skillData;

            if (referenceSkillData != null)
            {
                // TODO: change here with Localize
                skillNameText.text = Main.GameStaticDataManager.GetGameData<Data.ContextData>(referenceSkillData.NameID).zh_tw;
                button.interactable = referenceSkillData.Commands.Contains(Combat.Const.OnActived);
            }

            gameObject.SetActive(referenceSkillData != null);
        }

        public void Button_OnPressed()
        {
            OnPressed?.Invoke(referenceSkillData);
        }
    }
}