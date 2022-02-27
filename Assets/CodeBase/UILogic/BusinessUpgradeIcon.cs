using CodeBase;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UltEvents;

public class BusinessUpgradeIcon : MonoBehaviour
{
    [SerializeField] private UltEvent _onCollect;

    [SerializeField] private PlayerStats _playerStats;

    [SerializeField] private Image _coinImage;

    private BusinessUpgradeMenu _upgradeMenu;
    private Business _business;

    private float _deltaTime;

    private bool _canCollect;

    public void Update()
    {
        if(_deltaTime >= _business.EarningDurication)
        {
            _canCollect = true; 

            return;
        }

        _deltaTime += Time.deltaTime;

        _coinImage.fillAmount = Mathf.Clamp(_deltaTime / (_business.EarningDurication / 100f) / 100f, 0f, 1f);
    }

    public void TryCollect()
    {
        if(_canCollect)
        {
            _onCollect.Invoke();
        }
    }

    public void ResetEarning()
    {
        _deltaTime = 0;

        _canCollect = true;
    }

    public void AddMoney()
    {
        _playerStats.AddMoney(_business.Earning);
    }

    public void ShowUpgradeMenu()
    {
        _upgradeMenu.Initialize(_business);
    }

    public void Initialize(Business business, BusinessUpgradeMenu upgradeMenu)
    {
        _upgradeMenu = upgradeMenu;
        _business = business;
    }
}