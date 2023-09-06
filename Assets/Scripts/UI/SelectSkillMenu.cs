using ProjectBS.Combat;
using System;
using UnityEngine;

namespace ProjectBS.UI
{
    public class SelectSkillMenu : MonoBehaviour
    {
        [SerializeField] private GameObject root;

        private void Awake()
        {
            Combat.UnitTurnStartState.OnTurnStarted += UnitTurnStartState_OnTurnStarted;
        }

        private void UnitTurnStartState_OnTurnStarted(CombatActor obj)
        {
            throw new NotImplementedException();
        }
    }
}
