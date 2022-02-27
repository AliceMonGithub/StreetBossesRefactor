using UnityEngine;

namespace InputService
{
    public class StandaloneMovementInput : MovementInput
    {
        public override Vector2 Axis => GetHorizontalAxis();

        private Vector2 GetHorizontalAxis() =>
            Vector2.right * Input.GetAxisRaw(HorizontalAxis);
    }
}
