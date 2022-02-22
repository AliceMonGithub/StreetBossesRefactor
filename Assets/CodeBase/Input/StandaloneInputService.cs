using UnityEngine;

namespace CodeBase.Input
{
    public class StandaloneInputService : IInputSevice
    {
        public Vector2 Axis => GetAxis();

        private Vector2 GetAxis()
        {
            if (UnityEngine.Input.touches.Length == 0)
                return new Vector2(UnityEngine.Input.GetAxisRaw("Horizontal"), 0);

            return UnityEngine.Input.touches[0].deltaPosition;
        }
    }
}