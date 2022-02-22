using CodeBase;
using UnityEngine;

[CreateAssetMenu]
public class Business : ScriptableObject
{
    public string Name;

    [Space] public int Cost;

    public Character WorkingCharacter;

    public Character[] EnemyCharacters;

    [HideInInspector] public Business CurrentBusiness;

    [Space] [Header("Upgrade")]
    public Sprite Sprite;

    public float CollectTime;

    public int UpgradeCost;
    public int CollectAmount;
    public int CollectAmountIncrease;

    [Space]

    public int UpgradeProgress;
    public int UpgradeLevel;
    public int MaxUpgradeLevel;
    public int Increase;
}