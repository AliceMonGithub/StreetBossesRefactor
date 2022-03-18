using Assets.CodeBase;
using CodeBase;
using UltEvents;
using UnityEngine;

public class BusinessActionsIcon : MonoBehaviour
{
    [SerializeField] private UltEvent _onClick;
    [SerializeField] private UltEvent _onBusinessFound;
    [SerializeField] private UltEvent _onInitializeUpgradeIcon;

    [SerializeField] private Business _business;
    [SerializeField] private PlayerStats _playerStats;

    [Space]

    [SerializeField] private BigBusiness _bigBusiness;

    [Space]

    [SerializeField] private BusinessUpgradeIcon[] _icons;

    [Space]


    [SerializeField] private BusinessActionPanel _businessPanel;
    [SerializeField] private BusinessUpgradeMenu _upgradeMenu;
    [SerializeField] private BusinessUpgradeIcon _upgradeIcon;

    [Space]

    [SerializeField] private Transform _transform;

    private void Awake()
    {
        CheckBusiness();
    }

    public void ShowBusinessMenu()
    {
        _businessPanel.Initialize(_business, this);
    }

    public void OnClick()
    {
        _onClick.Invoke();
    }

    public void InitializeUpgradeIcon()
    {
        _onInitializeUpgradeIcon.Invoke();
    }

    public void InstantiateUpgradeIcon()
    {
        var upgradeMenu = Instantiate(_upgradeIcon, _transform.position, Quaternion.identity, transform.root);

        upgradeMenu.Initialize(_business, _upgradeMenu, _bigBusiness, _icons);
    }

    public void SetAttackBusiness()
    {
        _playerStats.AttackingBusiness = _business;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    private void CheckBusiness()
    {
        if(_playerStats.TryFindBusiness(_business))
        {
            _onBusinessFound.Invoke();
        }
    }
}