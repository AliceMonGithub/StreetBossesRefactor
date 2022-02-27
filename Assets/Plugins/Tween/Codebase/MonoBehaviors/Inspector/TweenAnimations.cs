using Tween.Animations;
using UnityEngine;

namespace Tween.Inspector
{
    public class TweenAnimations : MonoBehaviour
    {
        public void Play()
        {
            var animations = GetComponents<TweenAnimation>();

            if (animations == null) return;

            foreach(var animation in animations)
            {
                animation.Play();
            }
        }

        public void Stop()
        {
            var animations = GetComponents<TweenAnimation>();

            if (animations == null) return;

            foreach (var animation in animations)
            {
                animation.Stop();
            }
        }
    }
}