using System;
using System.Collections.Generic;
using Tween.ThreadsWorker;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Tween
{
    public static class ImageExtensions
    {
        private static readonly Dictionary<Image, CompositeDisposable> _threadsPool = new Dictionary<Image, CompositeDisposable>();

        public static void ChangeColor(this Image image, Color newColor, float time, AnimationCurve curve = null, Action onFinishAction = null)
        {
            if (time < 0) Debug.LogError("Time can't be negative");

            var start = image.color;
            var deltaTime = 0f;

            var thread = _threadsPool.CreateThread(image);

            Observable.EveryUpdate().Subscribe(action =>
            {
                if (deltaTime >= 1)
                {
                    _threadsPool.ClearThread(image);

                    onFinishAction?.Invoke();
                }

                deltaTime += Time.deltaTime / time;

                image.color = Color.LerpUnclamped(start, newColor, curve == null ? deltaTime : curve.Evaluate(deltaTime));

            }).AddTo(thread.Value);
        }

        public static void StopTween(this Image image)
        {
            _threadsPool.ClearThread(image);
        }
    }
}
