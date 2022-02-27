using System;
using System.Collections;
using TMPro;
using UltEvents;
using UnityEngine;

namespace Tween.Animations.Colors
{
    public class TMPColor : TweenAnimation
    {
        [SerializeField] private bool _loop;
        [SerializeField] private bool _nonStartColor;

        [Space]

        [SerializeField] private Color _color;
        [Min(0), SerializeField] private float _time;

        [Space]

        [SerializeField] private UltEvent _onFinish;

        [Space]

        [SerializeField] private AnimationType _animationType;
        [SerializeField] private AnimationCurve _curve;

        [Space]

        [SerializeField] private TMP_Text _text;

        [HideInInspector, SerializeField] private Color _startColor;

        private void OnValidate()
        {
            if (_text == null) return;

            if (Playing)
            {
                Stop();
            }

            _startColor = _text.color;
        }

        private void OnDestroy()
        {
            Stop();
        }

        public override void Play()
        {
            Stop();

            Playing = true;

            _text.ChangeColor(_color, _time, _curve, OnFinish);
        }

        public override void Stop()
        {
            _text.StopTween();

            Revert();

            Playing = false;
        }

        public override void Revert()
        {
            if (_nonStartColor && Application.isPlaying) return;

            if (Playing)
            {
                _text.color = _startColor;
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