using System;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectBS.UI
{
    public class CombatActorCard : MonoBehaviour
    {
        [SerializeField] private Image characterImage;
        [SerializeField] private Image frameImage;
        [SerializeField] private Color playerSideColor;
        [SerializeField] private Color enemySideColor;
        [Header("Aniamtion Setting")]
        [SerializeField] private Color highlightStateColor;
        [SerializeField] private float highlightStateTransTime;
        [SerializeField] private float highlightStateStayTime;
        [SerializeField] private float scaleUpStateSize;
        [SerializeField] private float scaleUpStateTransTime;
        [SerializeField] private float fadeOutStateTime;
        [SerializeField] private float resumeStateTime;

        private enum AnimationState
        {
            None,
            Highlight,
            ScaleUp,
            FadeOut,
            Resume
        }

        private AnimationState curAnimationState;

        public void ShowWith(CombatUI.CombatActorUIInfo info)
        {
            transform.parent.gameObject.SetActive(true);
            frameImage.color = info.isPlayer ? playerSideColor : enemySideColor;
            characterImage.transform.localPosition = new Vector3(info.offset.x, info.offset.y);
            GameResource.GameResourceManager.LoadAsset<Sprite>(info.spriteAddress, OnSpriteLoaded);
        }

        private void OnSpriteLoaded(Sprite sprite)
        {
            characterImage.sprite = sprite;
            characterImage.SetNativeSize();
        }

        public void Hide()
        {
            transform.parent.gameObject.SetActive(false);
        }

        public void PlayFrameHightlightAnimation()
        {
            if (curAnimationState != AnimationState.None)
                return;


        }
    }
}