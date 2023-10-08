using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectBS.UI
{
    public class CombatActorGrid : MonoBehaviour
    {
        [SerializeField] private CombatActorCard[] combatActorCards;

        public void ShowWith(List<CombatUI.CombatActorUIInfo> combatActors, bool isPlayer)
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

        public void PlayAllFrameAnimation()
        {
            for (int i = 0; i < combatActorCards.Length; i++)
            {
                combatActorCards[i].PlayFrameHightlightAnimation();
            }
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public CombatActorCard GetCard(int index)
        {
            if (combatActorCards == null || index < 0 || index >= combatActorCards.Length)
                return null;

            return combatActorCards[index];
        }
    }
}