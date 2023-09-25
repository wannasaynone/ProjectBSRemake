using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectBS.UI
{
    public class CombatActorGrid : MonoBehaviour
    {
        [SerializeField] private CombatActorCard[] combatActorCards;

        public CombatActorCard GetCard(int index)
        {
            if (combatActorCards == null || index < 0 || index >= combatActorCards.Length)
                return null;

            return combatActorCards[index];
        }
    }
}