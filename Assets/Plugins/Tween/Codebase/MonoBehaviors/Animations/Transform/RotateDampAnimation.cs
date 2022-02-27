using System;
using UltEvents;
using UnityEngine;

namespace Tween.Animations.Transforming
{
    public class RotateDampAnimation : TweenAnimation
    {
        [SerializeField] private bool _loop;
        [SerializeField] private bool _nonStartRotation;
        [SerializeField] private bool _fromCurrentAngle;

        [Space]

        [SerializeField] private Vector3 _angle;

        [Space]

        [Min(0), SerializeField] private float _smooth;
        [Min(0), SerializeField] private float _maxSpeed;

        [Space]

        [SerializeField] private UltEvent _onFinish;

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

            _transform.RotateDamp(_angle, _smooth, _maxSpeed, OnFinish);
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