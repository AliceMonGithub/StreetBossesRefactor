using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using Tween.ThreadsWorker;

namespace Tween
{
    public static class CanvasGroupExtensions
    {
        private static readonly Dictionary<CanvasGroup, CompositeDisposable> _threadsPool = new Dictionary<CanvasGroup, CompositeDisposable>();

        public static void ChangeAlpha(this CanvasGroup canvasGroup, float newAlpha, float time, AnimationCurve curve = null, Action onFinishAction = null)
        {
            if (time < 0) Debug.LogError("Time can't be negative");

            var start = canvasGroup.alpha;

            var deltaTime = 0f;

            var thread = _threadsPool.CreateThread(canvasGroup);

            Observable.EveryUpdate().Subscribe(action =>
            {
                if (deltaTime >= 1)
                {
                    _threadsPool.ClearThread(canvasGroup);

                    onFinishAction?.Invoke();
                }

                deltaTime += Time.deltaTime / time;

                canvasGroup.alpha = Mathf.LerpUnclamped(start, newAlpha, curve == null ? deltaTime : curve.Evaluate(deltaTime)); //Color.Lerp(start, newColor, curve == null ? deltaTime : curve.Evaluate(deltaTime));

            }).AddTo(thread.Value);
        }

        public static void StopTween(this CanvasGroup canvasGroup)
        {
            _threadsPool.ClearThread(canvasGroup);
        }
    }
}
