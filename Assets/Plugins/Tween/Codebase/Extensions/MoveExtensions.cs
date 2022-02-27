using System;
using System.Collections.Generic;
using Tween.ThreadsWorker;
using UniRx;
using UnityEngine;

namespace Tween
{
    public static class MoveExtensions
    {
        private static readonly Dictionary<Transform, CompositeDisposable> _threadsPool = new Dictionary<Transform, CompositeDisposable>();

        public static void Move(this Transform transform, Vector3 end, float time, bool localPosition = false, bool fromCurrentPosition = false, AnimationCurve curve = null, Action onFinishAction = null)
        {
            if (time < 0) Debug.LogError("Time can't be negative");

            var startPosition = localPosition ? transform.localPosition : transform.position;

            var endPosition = fromCurrentPosition ? startPosition + end : end;

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

                if (localPosition)
                {
                    transform.localPosition = Vector3.LerpUnclamped(startPosition, endPosition, curve == null ? deltaTime : curve.Evaluate(deltaTime));
                }
                else
                {
                    transform.position = Vector3.LerpUnclamped(startPosition, endPosition, curve == null ? deltaTime : curve.Evaluate(deltaTime));
                }

            }).AddTo(thread.Value);
        }

        public static void MoveDamp(this Transform transform, Vector3 end, float smooth, float maxSpeed = Mathf.Infinity, Action onFinishAction = null)
        {
            if (smooth < 0) Debug.LogError("Smooth can't be negative");

            var velosity = Vector3.zero;

            var thread = _threadsPool.CreateThread(transform);

            Observable.EveryUpdate().Subscribe(action =>
            {
                if ((end - transform.position).magnitude <= 0.02f)
                {
                    transform.position = end;

                    _threadsPool.ClearThread(transform);

                    onFinishAction?.Invoke();
                }

                transform.position = Vector3.SmoothDamp(transform.position, end, ref velosity, smooth, maxSpeed);

            }).AddTo(thread.Value);
        }

        public static void StopMovingTween(this Transform transform)
        {
            _threadsPool.ClearThread(transform);
        }
    }
}