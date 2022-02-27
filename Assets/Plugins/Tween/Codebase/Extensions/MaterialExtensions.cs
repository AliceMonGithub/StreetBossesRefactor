using System;
using System.Collections.Generic;
using Tween.ThreadsWorker;
using UniRx;
using UnityEngine;

namespace Tween
{
    public static class MaterialExtensions
    {
        private static readonly Dictionary<Material, CompositeDisposable> _threadsPool = new Dictionary<Material, CompositeDisposable>();

        public static void ChangeColor(this Material material, Color newColor, float time, AnimationCurve curve = null, Action onFinishAction = null)
        {
            if (time < 0) Debug.LogError("Time can't be negative");

            var start = material.color;
            var deltaTime = 0f;

            var thread = _threadsPool.CreateThread(material);

            Observable.EveryUpdate().Subscribe(action =>
            {
                if (deltaTime >= 1)
                {
                    _threadsPool.ClearThread(material);

                    onFinishAction?.Invoke();
                }

                deltaTime += Time.deltaTime / time;
              
                material.color = Color.LerpUnclamped(start, newColor, curve == null ? deltaTime : curve.Evaluate(deltaTime));

            }).AddTo(thread.Value);
        }

        public static void StopTween(this Material material)
        {
            _threadsPool.ClearThread(material);
        }
    }
}
