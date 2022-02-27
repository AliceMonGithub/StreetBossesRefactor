using CodeBase;
using HeroLogic;
using UnityEngine;

[CreateAssetMenu]
public class Business : ScriptableObject
{
    [SerializeField] private Sprite _image;

    [Space]

    [SerializeField] private string _name;

    [Space]

    [Min(0), SerializeField] private int _cost;

    [Space]

    [SerializeField] private HeroAttack[] _enemyHeroes;

    [Space]

    [Min(0), SerializeField] private float _earningDurication;
    [Min(0), SerializeField] private int _earning;

    [Space]

    [Min(1), SerializeField] private int _level;
    [Min(1), SerializeField] private int _maxLevel;

    [Space]

    [Min(0), SerializeField] private int _upgradeCost;
    [Min(0), SerializeField] private int _earningUpgrade;

    [SerializeField, HideInInspector] private int _upgradeProgress;

    [Space]

    [SerializeField] private Hero _workingHero;

    public Sprite Image => _image;

    public string Name => _name;

    public int Cost => _cost;

    public HeroAttack[] EnemyHeroes => _enemyHeroes;

    public float EarningDurication => _earningDurication;
    public int Earning => _earning;

    public int Level => _level;
    public int MaxLevel => _maxLevel;

    public int UpgradeCost => _upgradeCost;
    public int EarningUpgrade => _earning;

    public int UpgradeProgress => _upgradeProgress;

    public Hero WorkingHero => _workingHero;


    public void Upgrade()
    {
        _upgradeProgress += 25;

        if(_upgradeProgress == 100)
        {
            _level++;
            _earning += _earningUpgrade;

            _upgradeProgress = 0;
        }
    }

    public void SetWorkingHero(Hero hero)
    {
        _workingHero?.SetWorking(null);

        _workingHero = hero;

        _workingHero?.SetWorking(this);
    }

    //public string Name;

    //[Space] public int Cost;

    //public Character WorkingCharacter;

    //public Character[] EnemyCharacters;

    //[HideInInspector] public Business CurrentBusiness;

    //[Space] [Header("Upgrade")]
    //public Sprite Sprite;

    //public float CollectTime;

    //public int UpgradeCost;
    //public int CollectAmount;
    //public int CollectAmountIncrease;

    //[Space]

    //public int UpgradeProgress;
    //public int UpgradeLevel;
    //public int MaxUpgradeLevel;
    //public int Increase;
}