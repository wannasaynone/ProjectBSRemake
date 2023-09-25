using System;
using UnityEngine;

namespace ProjectBS.UI
{
    public class CombatUI : MonoBehaviour
    {
        [SerializeField] private CombatActorGrid playerGrid;

        private void Awake()
        {
            Combat.CombatManager.OnCombatStarted += OnCombatStarted;
        }

        private void OnCombatStarted()
        {
            playerGrid.GetCard(0).SetUp(null);
            playerGrid.GetCard(1).SetUp(null);
            playerGrid.GetCard(2).SetUp(null);
        }
    }
}