using CodeBase;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _countText;

    [SerializeField] private Sprite _lockSprite;
    [SerializeField] private Sprite _unlockSprite;

    [SerializeField] private Button _collectMoneyObject;
    [SerializeField] private Image _collectMoneyObjectImage;

    [Space]

    [SerializeField] private ParticleSystem _particleSystem;

    [Space]

    private Business _business;

    private PlayerStats _playerStats;
    private UpgradeMenu _upgradeMenu;

    private float _collectingTime;
    private float _autoCollectTime;
    private bool _canCollect;

    private void Update()
    {
        if(!_canCollect)
        {
            _collectingTime += Time.deltaTime;

            _collectMoneyObjectImage.fillAmount = Mathf.Clamp(_collectingTime / (_business.CollectTime / 100f) / 100f, 0f, 1f);
        }

        if(_collectingTime >= _business.CollectTime)
        {
            ActiveCollectIcon();

            _countText.text = _business.CollectAmount.ToString();
        }

        if (_business.WorkingCharacter == null) return;

        if(_business.WorkingCharacter != null && _canCollect)
        {
            _autoCollectTime += Time.deltaTime;
        }

        if(_autoCollectTime >= _business.WorkingCharacter.AutoCollectionTime)
        {
            CollectMoney();
        }
    }

    public void Initialize(Business business, UpgradeMenu upgradeMenu, PlayerStats playerStats)
    {
        _playerStats = playerStats;
        _business = business;
        _upgradeMenu = upgradeMenu;
    }
    public void ShowUpgradeMenu()
    {
        _upgradeMenu.Show(_business, _playerStats);
    }

    public void CollectMoney()
    {
        _collectMoneyObject.enabled = false;

        _countText.text = string.Empty;

        Instantiate(_particleSystem, _collectMoneyObject.transform.position, Quaternion.identity);

        _image.sprite = _lockSprite;

        _playerStats.Money += _business.CollectAmount;

        _autoCollectTime = 0;

        _canCollect = false;
    }

    private void ActiveCollectIcon()
    {
        _collectMoneyObject.enabled = true;

        _image.sprite = _unlockSprite;

        _collectingTime = 0;

        _canCollect = true;
    }
}