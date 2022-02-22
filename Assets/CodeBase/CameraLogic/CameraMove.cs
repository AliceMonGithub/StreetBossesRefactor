using CodeBase.Input;
using UnityEngine;

namespace CodeBase.CameraLogic
{
    public class CameraMove : MonoBehaviour
    {
        [Header("Settings")] [SerializeField] private float _sensitivity;

        [SerializeField] private float _xLimit;

        [Header("Components")] [SerializeField]
        private Transform _cameraTransform;

        private IInputSevice _input;

        private void Awake()
        {
            _input = new StandaloneInputService();
        }

        private void Update()
        {
            Move();
        }

        private void OnValidate()
        {
            if (_sensitivity < 0) _sensitivity = 0;
        }

        private void Move()
        {
            _cameraTransform.Translate(Vector3.right * _input.Axis * (_sensitivity * Time.deltaTime));

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