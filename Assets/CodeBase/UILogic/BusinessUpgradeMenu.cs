using System.Collections;
using System.Collections.Generic;
using Assets.CodeBase;
using CodeBase;
using CodeBase.Mao_AddiotionUiScript;
using CodeBase.UILogic;
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
    [SerializeField] private TutorialInfo _tutorialInfo;

    [Space]

    [SerializeField] private SelectHeroInBusiness _selectManagerMenu;
    [SerializeField] private SelectHeroInSecurity _selectSecurityMenu;

    [Space]

    [SerializeField] private GameObject[] _securityImages;
    [SerializeField] private Image[] _securityHeroesImages;
    [SerializeField] private GameObject[] _securityIcons;

    [Space]

    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private TMP_Text _earningText;
    [SerializeField] private TMP_Text _costText;
    [SerializeField] private TMP_Text _upgradeEarningText;

    [SerializeField] private Slider _levelSlider;

   // [SerializeField] private TMP_Text _haveHeroText;
    [SerializeField] private Image _businessPreview;

    [SerializeField] private GameObject[] _icon;

    [SerializeField] private GameObject[] _nullIcon;
   // [SerializeField] private Image _heroImage;
    [SerializeField] private Sprite _heroNullSprite;

    [SerializeField] private GameObject _tringle;

    private BusinessImage _businessImage;
    private Business _business;

    public void Render()
    {
        _nameText.text = _business.Name;
        _levelText.text = _business.Level == _business.MaxLevel ? "Max" : _business.Level.ToString();
        _earningText.text = _business.Earning.ToString();
        _costText.text = _business.Cost.ToString();
        _upgradeEarningText.text = (_business.Earning + _business.EarningUpgrade).ToString();

        _levelSlider.maxValue = 75;
        _levelSlider.value = _business.UpgradeProgress;

        //_heroImage.sprite = _business.WorkingHero == null ? _heroNullSprite : _business.WorkingHero.Image;

        if(_business.WorkingHero != null)
        {
            _businessPreview.sprite = _business.WorkingHero.Image;

            foreach (var nullIcon in _nullIcon)
            {
                nullIcon.SetActive(false);
            }

            foreach (var icon in _icon)
            {
                icon.SetActive(true);
            }
        }
        else
        {
            foreach (var nullIcon in _nullIcon)
            {
                nullIcon.SetActive(true);
            }

            foreach (var icon in _icon)
            {
                icon.SetActive(false);
            }
        }

        // _haveHeroText.text = _business.WorkingHero == null ? "+" : "-";

        var index = 0;

        foreach (var securityImage in _securityImages)
        {
            securityImage.SetActive(true);
        }

        foreach(var securityIcon in _securityIcons)
        {
            securityIcon.SetActive(false);
        }

        foreach (var hero in _business.Security)
        {
            if(hero == null)
            {
                index++;

                continue;
            }

            _securityHeroesImages[index].sprite = hero.Hero.Image;
            _securityIcons[index].SetActive(true);

            _securityImages[index].SetActive(false);

            index++;
        }

        if(_tutorialInfo.ManagerTringleHelp)
        {
            _tringle.SetActive(true);

            _tutorialInfo.ManagerTringleHelp = false;
        }
    }

    public void OpenSelectMenu()
    {
        if(_business.WorkingHero != null)
        {
            _business.SetWorkingHero(null);

            Render();

            return;
        }

        _selectManagerMenu.Initialize(_business);
    }

    public void OpenSelectSecurityMenu(int index)
    {
        while (_business.Security.Count < 3)
        {
            _business.Security.Add(null);
        }

        if (_business.Security[index] != null)
        {
            _business.SetSecurity(null, index);

            Render();

            return;
        }

        _business.Security.RemoveAll(gangster => gangster == null);

        _selectSecurityMenu.Initialize(_business, index);
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