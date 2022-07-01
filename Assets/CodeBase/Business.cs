using Assets.CodeBase;
using CodeBase;
using CodeBase.BotLogic;
using HeroLogic;
using System.Collections.Generic;
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

    [SerializeField] private List<HeroAttack> _security;

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

    [SerializeField] private int _upgradeProgress;

    [Space]

    [SerializeField] private ReactiveProperty<Hero> _workingHero;

    [SerializeField] private UltEvent _onManagerSet;

    public Bot Bot;

    public BusinessImage BusinessImage;
    public BusinessUpgradeIcon UpgradeIcon;

    public string StreetName;

    public ReactiveCommand OnUpgrade = new ReactiveCommand();

    public Sprite Image => _image;

    public Sprite Background => _background;

    public string Name => _name;

    public int Cost => _cost;

    public List<HeroAttack> Security => _security;

    public float DamageMultiple => _damageMultiple;
    public float HealthMultiple => _healthMultiple;

    public float EarningDurication => _earningDurication;
    public int Earning { get => _earning; set => _earning = value; }

    public int Level => _level;
    public int MaxLevel => _maxLevel;

    public int UpgradeCost => _upgradeCost;
    public int EarningUpgrade => Earning;

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

    public bool SimulateAttack(List<Hero> heroes)
    {
        int heroesDamage = 0;
        int enemyHeroesDamage = 0;

        int heroesHealth = 0;
        int enemyHeroesHealth = 0;

        if(_security.Count == 0)
        {
            return true;
        }

        foreach (var hero in heroes)
        {
            heroesDamage += hero.HeroAttack.Damage;
            heroesHealth += hero.HeroAttack.Health;
        }

        foreach (var hero in _security)
        {
            enemyHeroesDamage += hero.Damage;
            enemyHeroesHealth += hero.Health;
        }

        if(enemyHeroesDamage == 0)
        {
            enemyHeroesDamage = 1;
        }

        if(enemyHeroesHealth == 0)
        {
            enemyHeroesHealth = 1;
        }

        // enemyHeroesDamage += Random.Range(19, 24);
        //enemyHeroesHealth += Random.Range(225, 260);

        var enemyHitsCount = heroesHealth / enemyHeroesDamage;
        var heroesHitsCount = enemyHeroesHealth / heroesDamage;

        if(enemyHitsCount == heroesHitsCount)
        {
            var victory = false;
            var random = Random.Range(0, 2);

            if(random == 1)
            {
                victory = true;
            }

            return victory;
        }

        if(enemyHitsCount > heroesHitsCount)
        {
            return true;
        }
        else
        {
            return false;
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

    public void SetSecurity(Hero hero, int index)
    {
        while(_security.Count < 3)
        {
            _security.Add(null);
        }
        
        if(hero == null)
        {
            _security[index].Hero.SecurityBusiness = null;

            _security[index] = null;

            _security.RemoveAll(gangster => gangster == null);
        }
        else
        {
            hero.SecurityBusiness = this;

            _security[index] = hero.HeroAttack;

            _security.RemoveAll(gangster => gangster == null);
        }
    }
}