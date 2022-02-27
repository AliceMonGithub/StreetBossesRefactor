using System;
using System.Collections;
using UltEvents;
using UnityEngine;

namespace Tween.Animations.Transforming
{
    public class ScaleAnimation : TweenAnimation
    {
        [SerializeField] private bool _loop;
        [SerializeField] private bool _nonStartScale;
        [SerializeField] private bool _fromCurrentScale;

        [Space]

        [SerializeField] private Vector3 _scale;

        [Space]

        [Min(0), SerializeField] private float _time;

        [Space]

        [SerializeField] private UltEvent _onFinish;

        [Space]

        [SerializeField] private AnimationType _animationType;
        [SerializeField] private AnimationCurve _curve;

        [Space]

        [SerializeField] private Transform _transform;

        [HideInInspector, SerializeField] private Vector3 _startScale;

        private void OnValidate()
        {
            if (_transform == null) return;

            if(Playing)
            {
                Stop();
            }

            _startScale = _transform.localScale;
        }

        private void OnDestroy()
        {
            Stop();
        }

        public override void Play()
        {
            Stop();

            Playing = true;

            _transform.ScaleSmooth(_scale, _time, _fromCurrentScale, _curve, OnFinish);
        }

        public override void Stop()
        {
            _transform.StopScaleTween();

            Revert();

            Playing = false;
        }

        public override void Revert()
        {
            if (_nonStartScale && Application.isPlaying) return;

            if (Playing)
            {
                _transform.localScale = _startScale;
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