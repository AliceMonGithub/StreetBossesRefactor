using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Tween;
using UltEvents;

namespace Tween.Animations.Colors
{
    public class CanvasGroupAlphaAnimation : TweenAnimation
    {
        [SerializeField] private bool _loop;
        [SerializeField] private bool _nonStartAlpha;

        [Space]
        
        [Min(0), SerializeField] private float _alpha;
        [Min(0), SerializeField] private float _time;

        [Space]

        [SerializeField] private UltEvent _onFinish;

        [Space]

        [SerializeField] private AnimationType _animationType;
        [SerializeField] private AnimationCurve _curve;

        [Space]

        [SerializeField] private CanvasGroup _canvasGroup;

        [SerializeField] private float _startAlpha;

        private void OnValidate()
        {
            if (_canvasGroup == null) return;

            if (Playing)
            {
                Stop();
            }

            if (Application.isEditor == false) return;

            _startAlpha = _canvasGroup.alpha;
        }

        private void OnDestroy()
        {
            Stop();
        }

        public override void Play()
        {
            Stop();

            Playing = true;

            _canvasGroup.ChangeAlpha(_alpha, _time, _curve, OnFinish);
        }

        public override void Stop()
        {
            _canvasGroup.StopTween();

            Revert();

            Playing = false;
        }

        public override void Revert()
        {
            if (_nonStartAlpha && Application.isPlaying) return;

            if (Playing)
            {
                _canvasGroup.alpha = _startAlpha;
            }
        }

        private void OnFinish()
        {
            if (Application.isPlaying == false) return;

            if (_loop)
            {
                Play();
            }
            
            _onFinish.Invoke();
        }
    }
}