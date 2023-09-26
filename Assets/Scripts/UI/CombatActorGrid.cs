using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectBS.UI
{
    public class CombatActorGrid : MonoBehaviour
    {
        [SerializeField] private CombatActorCard[] combatActorCards;

        public void ShowWith(List<Combat.CombatActor> combatActors)
        {
            for (int i = 0; i < combatActorCards.Length; i++)
            {
                combatActorCards[i].Hide();
            }

            for (int i = 0; i < combatActors.Count; i++)
            {
                if (i >= combatActorCards.Length)
                {
                    continue;
                }

                combatActorCards[i].ShowWith(combatActors[i]);
            }

            gameObject.SetActive(true);
        }

        public CombatActorCard GetCard(int index)
        {
            if (combatActorCards == null || index < 0 || index >= combatActorCards.Length)
                return null;

            return combatActorCards[index];
        }
    }
}