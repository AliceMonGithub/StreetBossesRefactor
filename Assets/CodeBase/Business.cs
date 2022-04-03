using Assets.CodeBase;
using CodeBase;
using HeroLogic;
using UltEvents;
using UniRx;
using UnityEngine;
using Zenject;

[CreateAssetMenu]
public class Business : ScriptableObject
{
    [SerializeField] private Sprite _image;

    [Space]

    [SerializeField] private Sprite _background;

    [Space]

    [SerializeField] private string _name;

    [Space]

    [Min(0), SerializeField] private int _cost;

    [Space]

    [SerializeField] private HeroAttack[] _enemyHeroes;

    [SerializeField] private float _damageMultiple;
    [SerializeField] private float _healthMultiple;

    [Space]

    [Min(0), SerializeField] private float _earningDurication;
    [Min(0), SerializeField] private int _earning;

    [Space]

    [Min(1), SerializeField] private int _level;
    [Min(1), SerializeField] private int _maxLevel;

    [Space]

    [Min(0), SerializeField] private int _upgradeCost;
    [Min(0), SerializeField] private int _earningUpgrade;

    [Space]

    [SerializeField] private int _index;

    [SerializeField, HideInInspector] public BusinessImage BusinessImage;

    [SerializeField] public string StreetName;

    [SerializeField] private int _upgradeProgress;

    [Space]

    [SerializeField] private ReactiveProperty<Hero> _workingHero;

    [SerializeField] private UltEvent _onManagerSet;

    public ReactiveCommand OnUpgrade = new ReactiveCommand();

    public Sprite Image => _image;

    public Sprite Background => _background;

    public string Name => _name;

    public int Cost => _cost;

    public HeroAttack[] EnemyHeroes => _enemyHeroes;

    public float DamageMultiple => _damageMultiple;
    public float HealthMultiple => _healthMultiple;

    public float EarningDurication => _earningDurication;
    public int Earning => _earning;

    public int Level => _level;
    public int MaxLevel => _maxLevel;

    public int UpgradeCost => _upgradeCost;
    public int EarningUpgrade => _earning;

    public int UpgradeProgress => _upgradeProgress;

    public Hero WorkingHero => _workingHero.Value;
    public ReactiveProperty<Hero> WorkingHeroEvent => _workingHero;

    public int Index => _index;

    public void Upgrade()
    {
        _upgradeProgress += 25;

        if(_upgradeProgress == 100)
        {
            _level++;
            _earning += _earningUpgrade;

            _upgradeProgress = 0;

            OnUpgrade.Execute();
        }
    }

    public void SetWorkingHero(Hero hero)
    {
        _workingHero.Value?.SetWorking(null);

        _workingHero.Value = hero;

        _workingHero.Value?.SetWorking(this);

        if(_workingHero != null)
        {
            _onManagerSet.Invoke();
        }
    }
}