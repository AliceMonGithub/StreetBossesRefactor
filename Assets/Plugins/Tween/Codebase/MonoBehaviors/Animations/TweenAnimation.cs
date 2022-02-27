using Tween.Inspector;
using UnityEngine;

namespace Tween.Animations
{
    [RequireComponent(typeof(TweenAnimations))]
    public abstract class TweenAnimation : MonoBehaviour
    {
        public abstract void Play();
        public abstract void Stop();

        public abstract void Revert();

        protected bool Playing;
    }
}