using DG.Tweening;
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
        [SerializeField] private Color highlightStateColor_player;
        [SerializeField] private Color highlightStateColor_enemy;
        [SerializeField] private float highlightStateTransTime;
        [SerializeField] private float stayHighlightStateTime;
        [SerializeField] private float scaleUpStateSize;
        [SerializeField] private float resumeStateTime;
        [SerializeField] private float stayResumeStateTime;

        private bool isPlayer;

        private enum AnimationState
        {
            None,
            Highlight,
            StayHighlight,
            Resume,
            StayResume
        }

        private AnimationState curAnimationState;

        public void ShowWith(CombatUI.CombatActorUIInfo info)
        {
            isPlayer = info.isPlayer;
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

            curAnimationState = AnimationState.Highlight;
            Color targetColor = isPlayer ? highlightStateColor_player : highlightStateColor_enemy;
            frameImage.transform.DOScale(new Vector3(scaleUpStateSize, scaleUpStateSize, 1f), highlightStateTransTime);
            DOTween.To(GetFrameImageColor, SetFrameImageColor, targetColor, highlightStateTransTime).OnComplete(OnHighlightStateEnded);
        }

        private void OnHighlightStateEnded()
        {
            if (curAnimationState != AnimationState.Highlight)
                return;

            curAnimationState = AnimationState.StayHighlight;
            KahaGameCore.Common.TimerManager.Schedule(stayHighlightStateTime, OnStayHighlightStateEnded);
        }

        private void OnStayHighlightStateEnded()
        {
            if (curAnimationState != AnimationState.StayHighlight)
                return;

            curAnimationState = AnimationState.Resume;
            Color orginColor = isPlayer ? playerSideColor : enemySideColor;
            frameImage.transform.DOScale(Vector3.one, resumeStateTime);
            DOTween.To(GetFrameImageColor, SetFrameImageColor, orginColor, resumeStateTime).OnComplete(OnResumeStateEnde);
        }

        private void OnResumeStateEnde()
        {
            if (curAnimationState != AnimationState.Resume)
                return;

            curAnimationState = AnimationState.StayResume;
            KahaGameCore.Common.TimerManager.Schedule(stayResumeStateTime, OnStayResumeStateEnded);
        }

        private void OnStayResumeStateEnded()
        {
            if (curAnimationState != AnimationState.StayResume)
                return;

            curAnimationState = AnimationState.None;
            PlayFrameHightlightAnimation();
        }

        private Color GetFrameImageColor()
        {
            return frameImage.color;
        }

        private void SetFrameImageColor(Color color)
        {
            frameImage.color = color;
        }

        private void SetAlpha(float a)
        {
            frameImage.color = new Color(frameImage.color.r, frameImage.color.g, frameImage.color.b, a);
        }

        private float GetAlpha()
        {
            return frameImage.color.a;
        }
    }
}