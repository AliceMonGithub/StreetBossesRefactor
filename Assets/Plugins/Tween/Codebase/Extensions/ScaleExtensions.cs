using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tween.ThreadsWorker;
using UniRx;
using UnityEngine;

namespace Tween
{
    public static class ScaleExtensions
    {
        private static readonly Dictionary<Transform, CompositeDisposable> _threadsPool = new Dictionary<Transform, CompositeDisposable>();

        public static void ScaleSmooth(this Transform transform, Vector3 end, float time, bool fromCurrentScale = false, AnimationCurve curve = null, Action onFinishAction = null)
        {
            if (time < 0) Debug.LogError("Time can't be negative");

            var startScale = transform.localScale;

            var endScale = fromCurrentScale ? startScale + end : end;

            var deltaTime = 0f;

            var thread = _threadsPool.CreateThread(transform);

            Observable.EveryUpdate().Subscribe(action =>
            {
                if (deltaTime >= 1)
                {
                    _threadsPool.ClearThread(transform);

                    onFinishAction?.Invoke();
                }

                deltaTime += Time.deltaTime / time;

                transform.localScale = Vector3.LerpUnclamped(startScale, endScale, curve == null ? deltaTime : curve.Evaluate(deltaTime));

            }).AddTo(thread.Value);
        }

        public static void ScaleDamp(this Transform transform, Vector3 end, float smooth, float maxSpeed = Mathf.Infinity, Action onFinishAction = null)
        {
            if (smooth < 0) Debug.LogError("Smooth can't be negative");

            var velosity = Vector3.zero;

            var thread = _threadsPool.CreateThread(transform);

            Observable.EveryUpdate().Subscribe(action =>
            {
                if ((end - transform.localScale).magnitude <= 0.02f)
                {
                    transform.localScale = end;

                    _threadsPool.ClearThread(transform);

                    onFinishAction?.Invoke();
                }

                transform.localScale = Vector3.SmoothDamp(transform.localScale, end, ref velosity, smooth, maxSpeed);

            }).AddTo(thread.Value);
        }

        public static void StopScaleTween(this Transform transform)
        {
            _threadsPool.ClearThread(transform);
        }

    }
}
