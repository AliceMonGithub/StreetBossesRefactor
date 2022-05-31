using InputService;
using UniRx;
using UnityEngine;
using Zenject;

namespace CodeBase.CameraLogic
{
    public class CameraMovement : MonoBehaviour
    {
        [Header("Settings")] 

        [Min(0), SerializeField] private float _sensitivity;
        [Min(0), SerializeField] private float _moveSmooth;

        [SerializeField] private float _moveTime = 2f;

        [SerializeField] private float _xLimit;

        [Header("Components")]
        
        [SerializeField] private Transform _cameraTransform;

        private MovementInput _input = new StandaloneMovementInput();

        private Vector2 _axis;
        private Vector2 _velosity;

        private CompositeDisposable _disposable = new CompositeDisposable();

        private bool _enabled = true;

        private void Update()
        {
            if (_enabled == false) return;

            Move();
        }

        public void MoveToTarget(Transform target)
        {
            _enabled = false;

            var startPosition = _cameraTransform.position;

            var deltaTime = 0f;

            Observable.EveryUpdate().Subscribe(action =>
            {
                if(deltaTime >= 1)
                {
                    _enabled = true;

                    _disposable.Clear();
                }

                deltaTime += Time.deltaTime / _moveTime;

                var lerp = Vector3.Lerp(startPosition, target.position, deltaTime);

                _cameraTransform.position = new Vector3(Mathf.Clamp(lerp.x, -_xLimit, _xLimit), _cameraTransform.position.y, _cameraTransform.position.z);

            }).AddTo(_disposable);
        }

        private void Move()
        {
            _axis = Vector2.SmoothDamp(_axis, _input.Axis, ref _velosity, _moveSmooth);

            _cameraTransform.Translate((_sensitivity * Time.deltaTime) * _axis.x * Vector3.right);

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