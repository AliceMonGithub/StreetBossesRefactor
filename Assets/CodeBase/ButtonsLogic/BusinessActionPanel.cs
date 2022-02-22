using CodeBase;
using TMPro;
using UltEvents;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

internal class BusinessActionPanel : MonoBehaviour
{
    [SerializeField] private Image _businessImage;
    [SerializeField] private TMP_Text _costText;
    [SerializeField] private Business _currentBusiness;
    [SerializeField] private PlayerStats _playerStats;

    private BusinessActions _businessActions;

    private SceneLoader _sceneLoader;

    public UltEvent OnShow;
    public UltEvent OnHide;

    [Inject]
    private void Construct(SceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
    }

    public void Show(Business business, BusinessActions businessActions)
    {
        _businessImage.sprite = business.Sprite;
        _costText.text = business.Cost.ToString();

        _businessActions = businessActions;

        _currentBusiness.CurrentBusiness = business;

        _currentBusiness.EnemyCharacters = business.EnemyCharacters;
    
        OnShow.Invoke();
    }

    public void AttackBusiness()
    {
        _playerStats.CurrentSceneName = _sceneLoader.GetActiveScene().name;

        _sceneLoader.LoadScene("Battle");
    }

    public void TryBuyBusiness()
    {
        if (_playerStats.Money >= _currentBusiness.CurrentBusiness.Cost)
        {
            BuyBusiness();

            _businessActions.CheckBusiness();
        }
    }

    private void BuyBusiness()
    {
        _playerStats.Money -= _currentBusiness.CurrentBusiness.Cost;

        _playerStats.Businesses.Add(_currentBusiness.CurrentBusiness);

        gameObject.SetActive(false);
    }

    public void Hide() => OnHide.Invoke();
}