using System;
using UnityEngine;

namespace HeroLogic
{
    public class Hero : MonoBehaviour
    {
        [SerializeField] private Sprite _image;

        [Space]

        [SerializeField] private string _name;

        [Min(1), SerializeField] private int _level;

        [SerializeField] private HeroLevel[] _levelsProperties;

        [Space]

        [SerializeField] private PatrolMovement _patrolHero;
        
        [SerializeField] private HeroAttack _heroAttack;

        [SerializeField, HideInInspector] private Business _business;

        [SerializeField, HideInInspector] private int _upgradeProgress;

        public float Velosity { get; set; }

        public int UpgradeCost => _levelsProperties[Level - 1].Cost;

        public Sprite Image => _image;

        public string Name => _name;

        public int Level => _level;

        public HeroLevel[] LevelsProperties => _levelsProperties;

        public PatrolMovement PatrolHero => _patrolHero;

        public HeroAttack HeroAttack => _heroAttack;

        public Business Business => _business;

        public int UpgradeProgress => _upgradeProgress;

        public void TryUpgrade()
        {
            _upgradeProgress += 25;

            if(_upgradeProgress == 100)
            {
                var properties = _levelsProperties[Level];

                _heroAttack.SetHealth(properties.Health);
                _heroAttack.SetDamage(properties.Damage);
                _heroAttack.SetSpeed(properties.Speed);

                _level++;
                _upgradeProgress = 0;
            }
        }

        public void SetWorking(Business business)
        {
            _business = business;
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