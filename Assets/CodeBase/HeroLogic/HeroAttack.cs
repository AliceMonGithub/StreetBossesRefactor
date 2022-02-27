using Assets.CodeBase;
using System.Collections;
using UltEvents;
using UniRx;
using UnityEngine;

namespace HeroLogic
{
    public class HeroAttack : MonoBehaviour
    {
        private const float AttackDistance = 0.65f;

        [SerializeField] private UltEvent _onAttack;
        [SerializeField] private UltEvent _onHealthChanged;
        [SerializeField] private UltEvent _onDamage;
        [SerializeField] private UltEvent _onDie;

        [Min(0), SerializeField] private float _speed;
        [Min(0), SerializeField] private float _moveSmooth;

        [SerializeField] private ReactiveProperty<int> _health;
        [Min(0), SerializeField] private int _damage;

        [Space]

        [SerializeField] private Transform _transform;

        [Space]

        [SerializeField] private HeroAttack _target;
        [SerializeField] private Hero _hero;

        private Vector3 _velosity;

        private bool _recharge;

        public SpawnPoint SpawnPoint { get; set; }

        private bool CanAttack => Vector3.Distance(_transform.position, _target.Transform.position) <= AttackDistance;

        public Transform Transform => _transform;

        public int Health => _health.Value;
        public int Damage => _damage;

        private Vector3 _lastPosition;

        private float Speed => Vector3.Distance(_lastPosition, _transform.position);

        private void Awake()
        {
            _health.Subscribe(action => _onHealthChanged.Invoke());
        }

        public void Update()
        {
            _hero.Velosity = Speed * 1000;

            _lastPosition = _transform.position;

            if (_target == null) return;

            if (CanAttack && _recharge == false)
            {
                _onAttack.Invoke();

                return;
            }

            if(CanAttack)
            {
                return;
            }

            _transform.position = Vector3.SmoothDamp(_transform.position, _target.Transform.position, ref _velosity, _moveSmooth, _speed);
        }

        public void CheckHealth()
        {
            if(_health.Value <= 0)
            {
                _onDie.Invoke();
            }
        }

        public void StartRecharge()
        {
            _recharge = true;
        }

        public void StopRecharge()
        {
            _recharge = false;
        }

        public void Hit()
        {
            var EnemyDie = _target.DecreaseHealth();
        
            if(EnemyDie)
            {
                _target = null;
            }
        }

        public bool DecreaseHealth()
        {
            _health.Value -= _damage;

            _onDamage.Invoke();

            if(_health.Value <= 0)
            {
                return true;
            }

            return false;
        }

        public void SetHealth(int value)
        {
            _health.Value = value;
        }

        public void SetDamage(int value)
        {
            _damage = value;
        }

        public void SetSpeed(float value)
        {
            _speed = value;
        }
    }
}