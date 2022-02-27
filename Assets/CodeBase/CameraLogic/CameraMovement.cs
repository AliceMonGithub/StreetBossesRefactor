using InputService;
using UnityEngine;
using Zenject;

namespace CodeBase.CameraLogic
{
    public class CameraMovement : MonoBehaviour
    {
        [Header("Settings")] 

        [Min(0), SerializeField] private float _sensitivity;
        [SerializeField] private float _xLimit;

        [Header("Components")]
        
        [SerializeField] private Transform _cameraTransform;

        private MovementInput _input;

        [Inject]
        private void Construct(MovementInput input)
        {
            _input = input;
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            _cameraTransform.Translate(Vector3.right * _input.Axis.x * (_sensitivity * Time.deltaTime));

            LimitMove();
        }

        private void LimitMove()
        {
            var position = _cameraTransform.position;
            var x = Mathf.Clamp(position.x, -_xLimit, _xLimit);

            _cameraTransform.position = new Vector3(x, position.y, -10);
        }
    }
}