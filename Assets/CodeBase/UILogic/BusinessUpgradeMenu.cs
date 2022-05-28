using System.Collections;
using System.Collections.Generic;
using Assets.CodeBase;
using CodeBase;
using CodeBase.Mao_AddiotionUiScript;
using TMPro;
using UltEvents;
using UnityEngine;
using UnityEngine.UI;

public class BusinessUpgradeMenu : MonoBehaviour
{
    [SerializeField] private UltEvent _onInitialize;
    [SerializeField] private UltEvent _onUpgrade;

    [Space]

    [SerializeField] private PlayerStats _playerStats;

    [Space]

    [SerializeField] private SelectHeroInBusiness _selectMenu;

    [Space]

    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private TMP_Text _earningText;
    [SerializeField] private TMP_Text _costText;
    [SerializeField] private TMP_Text _upgradeEarningText;
    [SerializeField] private TMP_Text _managerText;

    [SerializeField] private Slider _levelSlider;

    [SerializeField] private Image _heroImage;
    [SerializeField] private Sprite _heroNullSprite;

    private BusinessImage _businessImage;
    private Business _business;

    public void Render()
    {
        _nameText.text = _business.Name;
        _levelText.text = _business.Level == _business.MaxLevel ? "Max" : _business.Level.ToString();
        _earningText.text = _business.Earning.ToString();
        _costText.text = _business.Cost.ToString();
        _upgradeEarningText.text = (_business.Earning + _business.EarningUpgrade).ToString();
        _managerText.text = _business.WorkingHero != null ? "-" : "+";

        _levelSlider.maxValue = 75;
        _levelSlider.value = _business.UpgradeProgress;

        _heroImage.sprite = _business.WorkingHero == null ? _heroNullSprite : _business.WorkingHero.Image;
    }

    public void OpenSelectMenu()
    {
        _selectMenu.Initialize(_business);
    }

    public void TryBuyUpgrade()
    {
        if (_business.MaxLevel == _business.Level) return;

        if(_business.UpgradeCost * 0.25f <= _playerStats.Money.Value)
        {
            _onUpgrade.Invoke();
        }
    }

    public void UpgradeBusiness()
    {
        _business.Upgrade();
    }

    public void UpgradeEffectImage()
    {
        _businessImage.OnUpgrade.Invoke();
    }

    public void SpendMoney()
    {
        _playerStats.Money.Value -= (int)Mathf.Round(_business.UpgradeCost * 0.25f);
    }

    public void Initialize(Business business, BusinessImage businessImage)
    {
        _business = business;
        _businessImage = businessImage;

        _onInitialize.Invoke();
    }
}