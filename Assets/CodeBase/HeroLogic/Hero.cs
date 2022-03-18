using CodeBase;
using System;
using UniRx;
using UnityEngine;

namespace HeroLogic
{
    public class Hero : MonoBehaviour
    {
        [SerializeField] private Sprite _image;

        [Space]

        [SerializeField] private string _name;

        [Space]

        [SerializeField] private SkillEffect _skill;
        [SerializeField] private float _skillReloadingTime;

        private float _currentTime;

        [Space]

        [SerializeField] private float _autoCollectTime;

        [Space]

        [SerializeField] private int _maxEnergy;

        [Space]

        [SerializeField] private int _chargeTime;

        [Space]

        [Min(1), SerializeField] private int _level;

        [SerializeField] private HeroLevel[] _levelsProperties;

        [Space]

        [SerializeField] private PatrolMovement _patrolHero;
        
        [SerializeField] private HeroAttack _heroAttack;

        [SerializeField] private SpriteRenderer _spriteRenderer;

        [Space]

        [SerializeField] private Transform _healthBarText;

        [SerializeField, HideInInspector] private Business _business;

        [SerializeField] private float _currentChargeTime;

        [SerializeField] private int _upgradeProgress;

        private CompositeDisposable _disposable = new CompositeDisposable();
        private CompositeDisposable _collectDisposable = new CompositeDisposable();

        public float Velosity { get; set; }

        public bool IsPlayerHero { get; set; }
        public bool Dead { get; set; }

        public bool SkillReloading { get; set; }

        public int UpgradeCost => _levelsProperties[Level - 1].Cost;

        public Sprite Image => _image;

        public string Name => _name;

        public SkillEffect Skill => _skill;

        public float SkillReloadingTime => _skillReloadingTime;

        public float CurrentTime => _currentTime;

        public float CurrentEnergyTime => _currentChargeTime;

        public float AutoCollectTime => _autoCollectTime;

        public int Energy => PlayerPrefs.GetInt($"Energy{Name}", _maxEnergy);
        public int MaxEnergy => _maxEnergy;

        public int Level => _level;

        public HeroLevel[] LevelsProperties => _levelsProperties;

        public PatrolMovement PatrolHero => _patrolHero;

        public HeroAttack HeroAttack => _heroAttack;

        public SpriteRenderer SpriteRenderer => _spriteRenderer;

        public Transform HealthbarText => _healthBarText;

        public Business Business => _business;

        public int UpgradeProgress => _upgradeProgress;

        public CompositeDisposable CollectDisposable => _collectDisposable;

        public void TryUpgrade()
        {
            _upgradeProgress += 25;

            if(_upgradeProgress == 100)
            {
                var properties = _levelsProperties[Level - 1];

                _heroAttack.SetHealth(properties.Health);
                _heroAttack.SetDamage(properties.Damage);
                _heroAttack.SetSpeed(properties.Speed);

                _level++;
                _upgradeProgress = 0;
            }
        }

        public float GetDistance(Vector3 position) =>
            Vector3.Distance(transform.position, position);

        public void SetWorking(Business business)
        {
            _business = business;
        }

        public void AddEnergy()
        {
            if(Energy < _maxEnergy)
            {
                PlayerPrefs.SetInt($"Energy{Name}", Energy + 1);
            }
        }

        public void SpendEnergy()
        {
            PlayerPrefs.SetInt($"Energy{Name}", Energy - 1);
        }

        public void SpendCurrentEnergyTime(float deltaTime)
        {
            _currentChargeTime -= deltaTime;

            if(_currentChargeTime <= 0)
            {
                AddEnergy();

                _currentChargeTime = _chargeTime;
            }
        }

        public void Die()
        {
            Dead = true;

            _heroAttack.enabled = false;
        }

        public void ReloadSkill()
        {
            SkillReloading = true;

            _currentTime = 0;

            Observable.EveryUpdate().Subscribe(action =>
            {
                _currentTime += Time.deltaTime;

                if (_currentTime >= _skillReloadingTime)
                {
                    SkillReloading = false;

                    _disposable.Dispose();
                }

            }).AddTo(_disposable);
        }
    }

    [Serializable]
    public class HeroLevel
    {
        [SerializeField] private int _cost;

        [Space]

        [SerializeField] private int _health;
        [SerializeField] private int _damage;
        [SerializeField] private float _speed;

        public int Cost => _cost;

        public int Health => _health;
        public int Damage => _damage;
        public float Speed => _speed;
    }
}