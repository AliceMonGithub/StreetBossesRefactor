using System.Collections;
using UnityEngine;

namespace Assets.CodeBase
{
    public class PauseBehavior : MonoBehaviour
    {
        public bool Paused { get; private set; }

        public void Pause()
        {
            Paused = !Paused;

            Time.timeScale = Paused ? 1f : 0f;
        }
    }
}