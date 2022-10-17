using DG.Tweening;
using UnityEngine;

namespace Cosmetics
{
    public class AnimateUI : MonoBehaviour
    {
        public RectTransform RectTransform;

        public Transform StartPoint;
        public float Duration;
        public Ease EaseType;
        public float JumpPower;
        public Vector2 ScalePower;

        private Vector3 _endPoint;
        private Tween _tween;
        private Sequence _sequence;

        private void Awake() =>
            _endPoint = Vector2.zero;

        public void Animate()
        {
            _sequence?.Kill();
            _sequence = DOTween.Sequence(this);

            RectTransform.localScale = Vector3.one;
            RectTransform.localPosition = StartPoint.localPosition;

            _sequence.Append(AnimateJump());
            _sequence.Join(AnimateScale());
        }

        private Tween AnimateJump() =>
            RectTransform
                .DOJumpAnchorPos(_endPoint, JumpPower, 1, Duration)
                .SetEase(EaseType)
                .OnComplete(() => RectTransform.anchoredPosition = Vector2.zero);

        private Tween AnimateScale() =>
            RectTransform
                .DOShakeScale(Duration, ScalePower, 0, 0, true, ShakeRandomnessMode.Harmonic)
                .SetDelay(_sequence.Duration() * 0.25f)
                .OnComplete(() => RectTransform.localScale = Vector3.one);
    }
}