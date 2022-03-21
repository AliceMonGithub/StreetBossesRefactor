using Assets.CodeBase;
using System.Collections;
using UltEvents;
using UniRx;
using UnityEngine;
using Zenject;

namespace HeroLogic
{
    public class HeroAttack : MonoBehaviour
    {
        [SerializeField] private UltEvent _onAttack;
        [SerializeField] private UltEvent _onHealthChanged;
        [SerializeField] private UltEvent _onDamage;
        [SerializeField] private UltEvent _onDie;

        [SerializeField] private UltEvent _onTargetNull;

        [Min(0), SerializeField] private float _speed;
        [Min(0), SerializeField] private float _moveSmooth;

        [SerializeField] private ReactiveProperty<int> _health;

        [Space]

        [Min(0), SerializeField] private int _damage;

        [SerializeField] private float _attackDistance = 0.65f;

        [Space]

        [SerializeField] private bool _meelCombat = true;
        [SerializeField] private GameObject _effect;

        [Space]

        [SerializeField] private Transform _transform;

        [Space]

        [SerializeField] private HeroAttack _target;
        [SerializeField] private Hero _hero;

        private Vector3 _velosity;

        private int _startHealth;

        private bool _recharge;

        public SpawnPoint SpawnPoint { get; set; }
        public AttackHeroes Heroes { get; set; }

        public bool IsEnemy { get; set; }

        private bool CanAttack => Vector3.Distance(_transform.position, _target.Transform.position) <= _attackDistance;

        public Transform Transform => _transform;

        public HeroAttack Target => _target;

        public Hero Hero => _hero;

        public int Health => _health.Value;
        public ReactiveProperty<int> HealthEvent => _health;

        public int StartHealth => _startHealth;

        public int Damage => _damage;

        private Vector3 _lastPosition;

        private float Speed => Vector3.Distance(_lastPosition, _transform.position);

        private void Awake()
        {
            _startHealth = _health.Value;

            _health.Subscribe(action => _onHealthChanged.Invoke());
        }

        private void Start()
        {
            if (Hero.Skill == null) return;

            Hero.Skill.Hero = this;
        }

        public void Update()
        {
            _hero.Velosity = Speed * 1000;

            _lastPosition = _transform.position;

            if (_target == null)
            {
                _onTargetNull.Invoke();

                return;
            }

            if (CanAttack && _recharge == false)
            {
                _onAttack.Invoke();

                return;
            }

            if (CanAttack)
            {
                return;
            }

            _transform.position = Vector3.SmoothDamp(_transform.position, _target.Transform.position, ref _velosity, _moveSmooth, _speed);

            Flip();
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
            var EnemyDie = _target?.DecreaseHealth(_damage);
        
            if(EnemyDie == true)
            {
                _target = null;
            }
        }

        public bool DecreaseHealth(int damage)
        {
            _health.Value -= damage;

            _onDamage.Invoke();

            if(_health.Value <= 0)
            {
                return true;
            }

            return false;
        }

        public void RemoveFromList()
        {
            Heroes.RemoveHero(_hero);
        }
        
        public void FindNewTarget()
        {
            if (_hero.Dead) return;
         
            if(_meelCombat == false)
            {
                
            }
            else
            {
                var nearHero = Heroes.FindNearHero(_hero);

                if (nearHero == null) return;

                _target = nearHero.HeroAttack;
            }
        }

        public void SetTarget(HeroAttack hero)
        {
            _target = hero;
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

        private void Flip()
        {
            var scale = _transform.localScale;

            if(_target._transform.position.x > _transform.position.x)
            {
                scale.x = -Mathf.Abs(scale.x);
            }
            else
            {
                scale.x = Mathf.Abs(scale.x);
            }

            _transform.localScale = scale;
        }
    }
}