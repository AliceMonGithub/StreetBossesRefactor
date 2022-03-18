using InputService;
using UnityEngine;
using Zenject;

namespace CodeBase.CameraLogic
{
    public class CameraMovement : MonoBehaviour
    {
        [Header("Settings")] 

        [Min(0), SerializeField] private float _sensitivity;
        [Min(0), SerializeField] private float _moveSmooth;
        [SerializeField] private float _xLimit;

        [Header("Components")]
        
        [SerializeField] private Transform _cameraTransform;

        private MovementInput _input = new StandaloneMovementInput();

        private Vector2 _axis;
        private Vector2 _velosity;

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            _axis = Vector2.SmoothDamp(_axis, _input.Axis, ref _velosity, _moveSmooth);

            _cameraTransform.Translate(Vector3.right * _axis.x * (_sensitivity * Time.deltaTime));

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