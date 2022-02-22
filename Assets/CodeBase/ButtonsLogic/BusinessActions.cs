using CodeBase;
using UnityEngine;

public class BusinessActions : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    [SerializeField] private BusinessActionPanel _actionPanel;
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private UpgradeButton _upgradeButton;
    [SerializeField] private UpgradeMenu _upgradeMenu;

    public Business Business;

    private void Awake()
    {
        CheckBusiness();
    }

    public void Show()
    {
        _actionPanel.Show(Business, this);
    }

    public void CheckBusiness()
    {
        if (_playerStats.Businesses.Find(business => business == Business) == null) return;

        var upgradeButton = Instantiate(_upgradeButton, _transform.position, Quaternion.identity, _transform.root);

        upgradeButton.Initialize(Business, _upgradeMenu, _playerStats);

        Destroy(gameObject);
    }
}