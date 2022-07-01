using Assets.CodeBase;
using CodeBase;
using CodeBase.QuestLogic;
using TMPro;
using UltEvents;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class BusinessUpgradeIcon : MonoBehaviour
{
    [SerializeField] private UltEvent _onCollect;

    [SerializeField] private PlayerStats _playerStats;

    [Space]

    [SerializeField] private GameObject[] _destroyingObjects;

    [SerializeField] private GameObject[] _hidingObjects;

    [Space]

    [SerializeField] private Image _coinImage;
    [SerializeField] private Image _heroIcon;

    [SerializeField] private TMP_Text _coinsAmountText;

    [Space]

    [SerializeField] private bool _nonInitialize;

    [Space]

    [SerializeField] private GameObject _particles;

    [SerializeField] private Quest _earningQuest;
    [SerializeField] private GameObject _coinTringle;
    [SerializeField] private GameObject _tringle;

    [Space]

    [SerializeField] private AudioSource _collectMusic;

    [SerializeField] private BigBusiness _bigBusiness;

    [SerializeField] private BusinessUpgradeMenu _upgradeMenu;
    [SerializeField] private BusinessActionsIcon _actionIcon;
    [SerializeField] private Business _business;

    [SerializeField] private BusinessImage _businessImage;

    private CompositeDisposable _disposable = new CompositeDisposable();

    private float _deltaTime;

    private int _coinsAmount;

    private bool CanCollect => _coinsAmount != 0;

    public UltEvent OnCollect => _onCollect;

    public Business Business => _business;

    private void Start()
    {
        foreach (var destroyingObject in _destroyingObjects)
        {
            Destroy(destroyingObject);
        }

        if (_nonInitialize)
        {
            Business.WorkingHeroEvent.Subscribe(action => Render()).AddTo(_disposable);
            Business.OnUpgrade.Subscribe(action => RefreshBigBusiness()).AddTo(_disposable);
        }
    }

    private void OnDisable()
    {
        _disposable.Clear();
    }

    private void Update()
    {
        if (_deltaTime >= _business.EarningDurication)
        {
            if (_business.WorkingHero == null)
            {
                _coinsAmount = _business.Earning;
            }
            else
            {
                _coinsAmount += _business.Earning;

                ResetEarning();
            }

            _coinsAmountText.text = _coinsAmount.ToString();
        }

        _deltaTime += Time.deltaTime;

        _coinImage.fillAmount = Mathf.Clamp(_deltaTime / (_business.EarningDurication / 100f) / 100f, 0f, 1f);
    }

    public void TryCollect()
    {
        if (CanCollect)
        {
            _onCollect.Invoke();

            _coinsAmountText.text = string.Empty;

            _coinsAmount = 0;

            CancelInvoke(nameof(TryCollect));
        }
    }

    public void InitializeActionIcon()
    {
        if (_actionIcon == null) return;

        var actionIcon = Instantiate(_actionIcon, transform.position, Quaternion.identity, transform.root);

        actionIcon.Initialize(_business);

        Destroy(gameObject);
    }

    public void ShowTringle()
    {
        _tringle.SetActive(true);
    }

    public void PlayCollectMusic()
    {
        _collectMusic.Play();
    }

    public void AddToCoinsAmount()
    {
        _coinsAmount += _coinsAmount;
    }

    public void ResetEarning()
    {
        _deltaTime = 0;
    }

    public void AddMoney()
    {
        _playerStats.Money.Value += _coinsAmount;
    }

    public void ShowUpgradeMenu()
    {
        _upgradeMenu.Initialize(_business, _businessImage);
    }

    public void RefreshBigBusiness()
    {
        if (_bigBusiness != null && Business.MaxLevel == Business.Level)
        {
            _bigBusiness.ShowNext(Business.Index + 1);
        }
    }

    public void InstantiateParticles()
    {
        Instantiate(_particles, transform.position, Quaternion.identity);
    }

    public void Render()
    {
        _heroIcon.enabled = false;

        if (_business.WorkingHero != null)
        {
            _heroIcon.sprite = _business.WorkingHero.Image;

            _heroIcon.enabled = true;
        }
    }

    public void Initialize(Business business, BusinessUpgradeMenu upgradeMenu, BigBusiness bigBusiness, BusinessImage businessImage, BusinessUpgradeIcon[] icons)
    {
        _upgradeMenu = upgradeMenu;
        _business = business;
        _businessImage = businessImage;

        _bigBusiness = bigBusiness;

        _business.BusinessImage = _businessImage;
        _business.UpgradeIcon = this;

        Business.WorkingHeroEvent.Subscribe(action => Render()).AddTo(_disposable);

        if (bigBusiness == null) return;

        _bigBusiness.Icons.Add(this);

        foreach (var icon in icons)
        {
            _bigBusiness.Icons.Add(icon);
        }

        RefreshBigBusiness();

        Business.OnUpgrade.Subscribe(action => RefreshBigBusiness()).AddTo(_disposable);

    }
}