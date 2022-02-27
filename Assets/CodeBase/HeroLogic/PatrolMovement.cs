using CodeBase;
using System.Collections;
using UltEvents;
using UnityEngine;

namespace HeroLogic
{
    public class PatrolMovement : MonoBehaviour
    {
        private const float StayDistance = 0.1f;

        [SerializeField] private UltEvent _onPointReached;
        [SerializeField] private UltEvent _onNewPointFound;

        [Min(0), SerializeField] private float _speed;
        [Min(0), SerializeField] private float _smooth;
        [Min(0), SerializeField] private float _stayTime;

        [SerializeField] private Transform[] _movePoints;

        [Space]

        [SerializeField] private Transform _transform;
        [SerializeField] private Hero _hero;

        private Transform _target;

        private Vector3 _lastPosition;
        private Vector3 _velosity;

        private bool _staying;

        private float Distance => Vector3.Distance(_transform.position, _target.position);
        private float Speed => Vector3.Distance(_lastPosition, _transform.position);

        private bool TargetIsBehind => _target.position.x > _transform.position.x;

        private void Awake()
        {
            _onPointReached.Invoke();
        }

        private void Update()
        {
            _hero.Velosity = Speed * 1000;

            _lastPosition = _transform.position;

            if (_target == null)
            {
                _onPointReached.Invoke();

                return;
            }

            _transform.position = Vector3.SmoothDamp(_transform.position, _target.position, ref _velosity, _smooth, _speed);

            if (Distance <= StayDistance)
            {
                _onPointReached.Invoke();
            }
        }

        public void Initialize(MovePoint movePoint)
        {
            _movePoints = movePoint.Points;
        }

        public void FindNewPoint()
        {
            if(_staying == false)
            {
                Invoke(nameof(SetRandomPoint), _stayTime);
            }
        }

        public void Flip()
        {
            var scale = _transform.localScale;

            if(TargetIsBehind)
            {
                scale.x = -Mathf.Abs(scale.x);
            }
            else
            {
                scale.x = Mathf.Abs(scale.x);
            }

            _transform.localScale = scale;
        }

        public void SetStaying(bool state)
        {
            _staying = state;
        }

        public void NullTarget()
        {
            _target = null;
        }

        private void SetRandomPoint()
        {
            _target = _movePoints[Random.Range(0, _movePoints.Length)];

            _onNewPointFound.Invoke();
        }
    }
}