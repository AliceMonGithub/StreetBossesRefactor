using System;
using System.Collections.Generic;
using Tween.ThreadsWorker;
using UniRx;
using UnityEngine;

namespace Tween
{
    public static class RotationExtensions
    {
        private static readonly Dictionary<Transform, CompositeDisposable> _threadsPool = new Dictionary<Transform, CompositeDisposable>();

        public static void RotateSmooth(this Transform transform, Vector3 end, float time, bool localRotation = false, bool fromCurrentRotation = false, AnimationCurve curve = null, Action onFinishAction = null)
        {
            if (time < 0) Debug.LogError("Time can't be negative");

            var startRotation = localRotation ? transform.localEulerAngles : transform.eulerAngles;

            var endPosition = fromCurrentRotation ? startRotation + end : end;

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

                if(localRotation)
                {
                    transform.localEulerAngles = Vector3.LerpUnclamped(startRotation, endPosition, curve == null ? deltaTime : curve.Evaluate(deltaTime));
                }
                else
                {
                    transform.eulerAngles = Vector3.LerpUnclamped(startRotation, endPosition, curve == null ? deltaTime : curve.Evaluate(deltaTime));
                }

            }).AddTo(thread.Value);
        }

        public static void RotateDamp(this Transform transform, Vector3 end, float smooth, float maxSpeed = Mathf.Infinity, Action onFinishAction = null)
        {
            if (smooth < 0) Debug.LogError("Speed can't be negative");

            var velosity = Vector3.zero;

            var thread = _threadsPool.CreateThread(transform);

            Observable.EveryUpdate().Subscribe(action =>
            {
                if ((end - transform.eulerAngles).magnitude <= 0.04f)
                {
                    transform.eulerAngles = end;

                    _threadsPool.ClearThread(transform);

                    onFinishAction?.Invoke();
                }

                transform.eulerAngles = Vector3.SmoothDamp(transform.eulerAngles, end, ref velosity, smooth, maxSpeed);

            }).AddTo(thread.Value);
        }

        public static void StopRotatingTween(this Transform transform)
        {
            _threadsPool.ClearThread(transform);
        }
    }
}
