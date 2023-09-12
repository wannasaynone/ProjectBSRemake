using ProjectBS.Combat;
using System;
using UnityEngine;

namespace ProjectBS.UI
{
    public class SelectSkillMenu : MonoBehaviour
    {
        [SerializeField] private GameObject root;
        [SerializeField] private SkillButton[] skillButtons;

        private void Awake()
        {
            Combat.UnitTurnStartState.OnTurnStarted += UnitTurnStartState_OnTurnStarted;
        }

        private void UnitTurnStartState_OnTurnStarted(CombatActor actor)
        {
            for (int i = 0; i < skillButtons.Length; i++)
            {
                skillButtons[i].SetUp(actor.GetSkillSource(i));
            }

            root.SetActive(true);
        }
    }
}
