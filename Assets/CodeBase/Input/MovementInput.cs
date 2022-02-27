using UnityEngine;

namespace InputService
{
    public abstract class MovementInput : IInputSevice
    {
        protected const string HorizontalAxis = "Horizontal";

        public abstract Vector2 Axis { get; }
    }
}