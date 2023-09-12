using UnityEngine;
using UnityEngine.UI;

namespace ProjectBS.UI
{
    public class SkillButton : MonoBehaviour
    {
        [SerializeField] private Text skillNameText;

        private Data.SkillData referenceSkillData;

        public void SetUp(Data.SkillData skillData)
        {
            referenceSkillData = skillData;

            if (referenceSkillData != null)
                // TODO: change here with Localize
                skillNameText.text = Main.GameStaticDataManager.GetGameData<Data.ContextData>(referenceSkillData.NameID).zh_tw;

            gameObject.SetActive(referenceSkillData != null);
        }
    }
}