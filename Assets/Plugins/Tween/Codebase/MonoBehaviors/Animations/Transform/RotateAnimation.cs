using System;
using UltEvents;
using UnityEngine;

namespace Tween.Animations.Transforming
{
    public class RotateAnimation : TweenAnimation
    {
        [SerializeField] private bool _loop;
        [SerializeField] private bool _nonStartRotation;
        [SerializeField] private bool _fromCurrentAngle;
        [SerializeField] private bool _localRotation;

        [Space]

        [SerializeField] private Vector3 _angel;

        [Space]

        [Min(0), SerializeField] private float _time;

        [Space]

        [SerializeField] private UltEvent _onFinish;

        [Space]

        [SerializeField] private AnimationType _animationType;
        [SerializeField] private AnimationCurve _curve;

        [Space]

        [SerializeField] private Transform _transform;

        [HideInInspector, SerializeField] private Vector3 _startRotation;

        private void OnValidate()
        {
            if (_transform == null) return;

            if (Playing)
            {
                Stop();
            }

            _startRotation = _transform.eulerAngles;
        }

        private void OnDestroy()
        {
            Stop();
        }

        public override void Play()
        {
            Stop();

            Playing = true;

            _transform.RotateSmooth(_angel, _time, _localRotation, _fromCurrentAngle, _curve, OnFinish);
        }

        public override void Stop()
        {
            _transform.StopRotatingTween();

            Revert();

            Playing = false;
        }

        public override void Revert()
        {
            if (_nonStartRotation && Application.isPlaying) return;

            if (Playing)
            {
                _transform.eulerAngles = _startRotation;
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