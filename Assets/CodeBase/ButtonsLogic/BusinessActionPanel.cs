using CodeBase;
using SceneLogic;
using TMPro;
using UltEvents;
using UnityEngine;
using Zenject;

internal class BusinessActionPanel : MonoBehaviour
{
    [SerializeField] private UltEvent _onBuy;
    [SerializeField] private UltEvent _onAttack;
    [SerializeField] private UltEvent _onInitialized;
    [SerializeField] private UltEvent _onHide;

    [SerializeField] private PlayerStats _playerStats;

    [SerializeField] private ERC20BalanceOfExample _balanceOf;
    [SerializeField] private Web3WalletTransfer20Example _transfer;
    [SerializeField] private GameObject _transferLoad;

    [SerializeField] private GameObject[] _botMenu;
    [SerializeField] private GameObject[] _nonBotMenu;

    [SerializeField] private TMP_InputField _inputField;

    [SerializeField] private TMP_Text _costText;

    private LoadCurtain _loadCurtain;

    private BusinessActionsIcon _icon;
    private Business _business;

    [Inject]
    private void Construct(LoadCurtain loadCurtain)
    {
        _loadCurtain = loadCurtain;
    }

    private void OnEnable()
    {
        if (_business.Bot != null)
        {
            foreach (var gObject in _nonBotMenu)
            {
                gObject.SetActive(false);
            }

            foreach (var gObject in _botMenu)
            {
                gObject.SetActive(true);
            }

            return;
        }

        foreach (var gObject in _nonBotMenu)
        {
            gObject.SetActive(true);
        }

        foreach (var gObject in _botMenu)
        {
            gObject.SetActive(false);
        }
    }

    public void Hide()
    {
        _onHide.Invoke();
    }

    public void CorrentNumber()
    {
        if (int.TryParse(_inputField.text, out int result) == false)
        {
            _inputField.text = string.Empty;
        }

        if (_playerStats.Money.Value < int.Parse(_inputField.text))
        {
            _inputField.text = _playerStats.Money.Value.ToString();
        }
    }

    public void TryBuy()
    {
        // _transferLoad.gameObject.SetActive(true);

        //var balance = await _balanceOf.GetBalance("0x0bf2BF9a44c9c7FAf2b8534d88d62401ee442Bad"); // paste st token contract

        if (_business.Bot != null)
        {
            if (int.Parse(_inputField.text) == 0) return;

            if (_playerStats.Money.Value < int.Parse(_inputField.text)) return;

            var result = _business.Bot.BuyRequest(_business, int.Parse(_inputField.text));

            if (result)
            {
                InitializeUpgradeIcon();

                Hide();
            }

            return;
        }

        if (_playerStats.Money.Value >= _business.Cost)
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
        _business.StreetName = _loadCurtain.CurrentSceneName;

        _playerStats.Add(_business);
    }

    public void Attack()
    {
        _onAttack.Invoke();
    }

    public void SetAttackBusiness()
    {
        _playerStats.LastSceneName = _loadCurtain.CurrentSceneName;
        _playerStats.AttackingBusiness = _business;
    }

    public void LoadBattleScene()
    {
        if (_playerStats.Heroes.Value.Count == 0) return;

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