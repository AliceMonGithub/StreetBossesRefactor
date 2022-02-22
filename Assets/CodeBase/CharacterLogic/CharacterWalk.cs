using System.Linq;
using UnityEngine;

//using Spine.Unity;

namespace CodeBase.CharactersLogic
{
    public class CharacterWalk : MonoBehaviour
    {
        private const string GoToRandomPointMethodName = "GoToRandomPoint";
        private const float Distance = 0.1f;

        public float WalkSpeed;
        
        [SerializeField] private float _stayTime;

        [Space] [SerializeField] private Transform[] _walkPoints;

        [SerializeField] private Transform _transform;
        [SerializeField] private Animator _animator;
        
        private bool _canWalk;

        private Transform _currentPoint;
        private int _currentPointIndex;
        private float _startXScale;
        
        private readonly int Walk = Animator.StringToHash("Walk");
        private Transform _lastPoint;

        private void Start()
        {    
            _startXScale = _transform.localScale.x;

            GoToRandomPoint();
        }

        private void Update()
        {
            if (_canWalk)
                _transform.position = Vector3.MoveTowards(_transform.position, _currentPoint.position,
                    WalkSpeed * Time.deltaTime);

            if ((_transform.position - _currentPoint.position).magnitude < Distance && _canWalk)
            {
                _canWalk = false;

                _animator.SetBool(Walk, false);

                Invoke(GoToRandomPointMethodName, _stayTime);
            }
        }

        private void GoToRandomPoint()
        {
            var possibleMoves = _walkPoints.Where(movePoint => _lastPoint != movePoint).ToArray();
        
            var randomPoint = possibleMoves[Random.Range(0, possibleMoves.Length)];
            
            _currentPoint = randomPoint;
            _lastPoint = randomPoint;

            Flip();

            _canWalk = true;

            _animator.SetBool(Walk, true);
        }

        private void Flip()
        {
            var scale = _transform.localScale;

            var isRight = FlipIsRight();
            float x;

            if (isRight)
            {
                x = -_startXScale;

                _transform.localScale = new Vector3(x, scale.y, scale.z);

                return;
            }

            x = _startXScale;

            _transform.localScale = new Vector3(x, scale.y, scale.z);
        }

        private bool FlipIsRight()
        {
            if (_currentPoint.position.x < _transform.position.x) return false;

            return true;
        }

        public void Initialize(Transform[] movePointPoints)
        {
            _walkPoints = movePointPoints;
        }
    }
}