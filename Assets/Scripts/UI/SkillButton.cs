using UnityEngine;
using UnityEngine.UI;

namespace ProjectBS.UI
{
    public class SkillButton : MonoBehaviour
    {
        public event System.Action<int> OnPressed;

        [SerializeField] private Text skillNameText;
        [SerializeField] private Button button;

        private int referenceSkillDataID;

        public class SkillButtonInfo
        {
            public string name;
            public int id;
            public bool isActiveSkill;
        }

        public void SetUp(SkillButtonInfo info)
        {
            referenceSkillDataID = info.id;

            if (referenceSkillDataID != -1)
            {
                skillNameText.text = info.name;
                button.interactable = info.isActiveSkill;
            }

            gameObject.SetActive(referenceSkillDataID != -1);
        }

        public void Button_OnPressed()
        {
            OnPressed?.Invoke(referenceSkillDataID);
        }
    }
}