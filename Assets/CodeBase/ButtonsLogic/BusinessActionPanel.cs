using CodeBase;
using SceneLogic;
using TMPro;
using UltEvents;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

internal class BusinessActionPanel : MonoBehaviour
{
    [SerializeField] private UltEvent _onBuy;
    [SerializeField] private UltEvent _onAttack;
    [SerializeField] private UltEvent _onInitialized;
    [SerializeField] private UltEvent _onHide;

    [SerializeField] private PlayerStats _playerStats;

    [SerializeField] private TMP_Text _costText;

    private LoadCurtain _loadCurtain;

    private BusinessActionsIcon _icon;
    private Business _business;

    [Inject]
    private void Construct(LoadCurtain loadCurtain)
    {
        _loadCurtain = loadCurtain;
    }

    public void Hide()
    {
        _onHide.Invoke();
    }

    public void TryBuy()
    {
        if(_playerStats.Money.Value >= _business.Cost)
        {
            _onBuy.Invoke();
        }
    }

    public void InitializeUpgradeIcon()
    {
        _icon.InitializeUpgradeIcon();
    }

    public void SpendMoney()
    {
        _playerStats.Money.Value -= _business.Cost;
    }

    public void AddBusinessToPlayerStats()
    {
        _playerStats.Businesses.Add(_business);
    }

    public void Attack()
    {
        _onAttack.Invoke();
    }

    public void SetAttackBusiness()
    {
        _playerStats.AttackingBusiness = _business;
    }

    public void LoadBattleScene()
    {
        _loadCurtain.LoadScene("Battle");
    }

    public void Render()
    {
        _costText.text = _business.Cost.ToString();
    }

    public void Initialize(Business business, BusinessActionsIcon icon)
    {
        _business = business;
        _icon = icon;

        _onInitialized.Invoke();
    }
}