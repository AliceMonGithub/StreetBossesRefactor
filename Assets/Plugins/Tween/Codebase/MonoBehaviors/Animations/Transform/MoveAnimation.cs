using System;
using UltEvents;
using UnityEngine;

namespace Tween.Animations.Transforming
{
    public class MoveAnimation : TweenAnimation
    {
        [SerializeField] private bool _loop;
        [SerializeField] private bool _nonStartPosition;
        [SerializeField] private bool _fromCurrentPosition;
        [SerializeField] private bool _localPosition;

        [Space]

        [SerializeField] private Vector3 _position;

        [Space]

        [Min(0), SerializeField] private float _time;

        [Space]

        [SerializeField] private UltEvent _onFinish;

        [Space]

        [SerializeField] private AnimationType _animationType;
        [SerializeField] private AnimationCurve _curve;

        [Space]

        [SerializeField] private Transform _transform;

        [HideInInspector, SerializeField] private Vector3 _startPosition;

        private void OnValidate()
        {
            if (_transform == null) return;

            if (Playing)
            {
                Stop();
            }

            _startPosition = _transform.position;
        }

        private void OnDestroy()
        {
            Stop();
        }

        public override void Play()
        {
            Stop();

            Playing = true;

            _transform.Move(_position, _time, _localPosition, _fromCurrentPosition, _curve, OnFinish);
        }

        public override void Stop()
        {
            _transform.StopMovingTween();

            Revert();

            Playing = false;
        }

        public override void Revert()
        {
            if (_nonStartPosition && Application.isPlaying) return;

            if (Playing)
            {
                _transform.position = _startPosition;
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