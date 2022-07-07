using Assets.CodeBase;
using CodeBase;
using CodeBase.BotLogic;
using CodeBase.UILogic;
using TMPro;
using UltEvents;
using UnityEngine;
using UnityEngine.UI;

public class BusinessActionsIcon : MonoBehaviour
{
    [SerializeField] private UltEvent _onClick;
    [SerializeField] private UltEvent _onBusinessFound;
    [SerializeField] private UltEvent _onInitializeUpgradeIcon;

    [SerializeField] private Business _business;
    [SerializeField] private PlayerStats _playerStats;

    [Space]

    [SerializeField] private Bot _bot;
    [SerializeField] private Image _image;

    [Space]

    [SerializeField] private BigBusiness _bigBusiness;

    [Space]

    [SerializeField] private BusinessUpgradeIcon[] _icons;

    [Space]

    [SerializeField] private GameObject[] _hidingObjects;
    [SerializeField] private GameObject[] _activatingObjects;

    [SerializeField] private TMP_Text _nickText;

    [SerializeField] private FillSimulation _fillSimulation;

    [Space]


    [SerializeField] private BusinessActionPanel _businessPanel;
    [SerializeField] private BusinessUpgradeMenu _upgradeMenu;
    [SerializeField] private BusinessUpgradeIcon _upgradeIcon;

    [Space]

    [SerializeField] private BusinessImage _businessImage;

    [Space]

    [SerializeField] private Transform _transform;

    private bool _withBot = false;

    private void Start()
    {
        _bot = FindObjectOfType<Bot>();

        _business.BusinessImage = _businessImage;

        if(_bot.FindBusiness(_business))
        {
            foreach(var gameObject in _hidingObjects)
            {
                gameObject.SetActive(false);
            }

            foreach (var gameObject in _activatingObjects)
            {
                gameObject.SetActive(true);
            }

            _fillSimulation.Initialize(_business, _bot);

            _nickText.gameObject.SetActive(true);

            _nickText.text = _bot.Nickname;

            _withBot = true;
        }

        CheckBusiness();
    }

    private void Update()
    {
        if(_business.Bot != null && _withBot == false)
        {
            foreach (var gameObject in _hidingObjects)
            {
                gameObject.SetActive(false);
            }

            foreach (var gameObject in _activatingObjects)
            {
                gameObject.SetActive(true);
            }

            _fillSimulation.Initialize(_business, _bot);

            _nickText.gameObject.SetActive(true);

            _nickText.text = _bot.Nickname;

            _withBot = true;
        }

        if(_business.Bot == null && _withBot)
        {
            foreach (var gameObject in _hidingObjects)
            {
                gameObject.SetActive(true);
            }

            foreach (var gameObject in _activatingObjects)
            {
                gameObject.SetActive(false);
            }

            _fillSimulation.Initialize(_business, _bot);

            _fillSimulation.gameObject.SetActive(false);

            _nickText.gameObject.SetActive(false);

            _nickText.text = string.Empty;

            _withBot = false;
        }
    }

    public void Initialize(Business business)
    {
        _business = business;

        _businessImage = business.BusinessImage;
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

        upgradeMenu.Initialize(_business, _upgradeMenu, _bigBusiness, _businessImage, _icons);
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