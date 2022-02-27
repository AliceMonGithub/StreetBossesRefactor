using System;
using System.Collections.Generic;
using TMPro;
using Tween.ThreadsWorker;
using UniRx;
using UnityEngine;

namespace Tween
{
    public static class TMPExtensions
    {
        private static readonly Dictionary<TMP_Text, CompositeDisposable> _threadsPool = new Dictionary<TMP_Text, CompositeDisposable>();

        public static void ChangeColor(this TMP_Text text, Color newColor, float time, AnimationCurve curve = null, Action onFinishAction = null)
        {
            if (time < 0) Debug.LogError("Time can't be negative");

            var start = text.color;
            var deltaTime = 0f;

            var thread = _threadsPool.CreateThread(text);

            Observable.EveryUpdate().Subscribe(action =>
            {
                if (deltaTime >= 1)
                {
                    _threadsPool.ClearThread(text);

                    onFinishAction?.Invoke();
                }

                deltaTime += Time.deltaTime / time;

                text.color = Color.LerpUnclamped(start, newColor, curve == null ? deltaTime : curve.Evaluate(deltaTime));

            }).AddTo(thread.Value);
        }

        public static void StopTween(this TMP_Text text)
        {
            _threadsPool.ClearThread(text);
        }
    }
}
