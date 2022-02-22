using System.Collections;
using System.Collections.Generic;
using CodeBase;
using CodeBase.Mao_AddiotionUiScript;
using TMPro;
using UltEvents;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    public CharacterSelectMenu CharacterSelectMenu;
    public Business CurrentBusiness;

    [SerializeField] private Sprite _startCharacterSelectSprite;

    [Min(0)][SerializeField] private float _timeForShowFullSlider;
    [SerializeField] private Slider _progressSlider;

    [SerializeField] private Image _characterIcon;

    [SerializeField] private TMP_Text _businessName;
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private TMP_Text _increaseText;
    [SerializeField] private TMP_Text _costText;
    [SerializeField] private TMP_Text _nextIncreaseText;

    public PlayerStats PlayerStats;

    public UltEvent OnShow;
    public UltEvent OnHide;
    public UltEvent<int> ProgressUpdate;
    public UltEvent OnHasWorkingCharacter;
    public UltEvent OnHasNotWorkingCharacter;

    public void Buy()
    {
        var cost = (int)Mathf.Round(CurrentBusiness.UpgradeCost * 0.25f);

        if (PlayerStats.Money >= cost && CurrentBusiness.MaxUpgradeLevel > CurrentBusiness.UpgradeLevel)
        {
            PlayerStats.Money -= cost;

            CurrentBusiness.UpgradeProgress += 25;
        }

        if(CurrentBusiness.UpgradeProgress == 100)
        {
            UpgradeLevel();
            
            CurrentBusiness.UpgradeProgress = 0;
        }

        UpdateData();
    }

    private void UpgradeLevel()
    {
            CurrentBusiness.UpgradeLevel++;

            CurrentBusiness.UpgradeCost += CurrentBusiness.Increase;

            CurrentBusiness.CollectAmount += CurrentBusiness.CollectAmountIncrease;

            UpdateData();
    }

    private void UpdateLevel()
    {
        _levelText.text = CurrentBusiness.UpgradeLevel + "/" + CurrentBusiness.MaxUpgradeLevel;
    }

    private void UpdateData()
    {
        UpdateLevel();
        UpdateProgress();
        
        var cost = Mathf.RoundToInt(CurrentBusiness.UpgradeCost * 0.25f);

        _increaseText.text = CurrentBusiness.Increase.ToString();
        _costText.text = cost.ToString();
        _nextIncreaseText.text = (CurrentBusiness.Increase + CurrentBusiness.CollectAmountIncrease).ToString();
    }

    private void UpdateProgress()
    {
        if (CurrentBusiness.UpgradeProgress == 0)
        {
            ProgressUpdate.Invoke(0);
            // Временный вариант, чтобы получить объет который может запускать корутины
            // Похорошему должен быть ICorutineStarter, который должен лежать в DI
            //GlobalCuratain.Instance.StartCoroutine(UpdateSliderTimer(_timeForShowFullSlider));
        }
        else
        {
            ProgressUpdate.Invoke(CurrentBusiness.UpgradeProgress);
        }
        //_progressSlider.value = CurrentBusiness.UpgradeProgress;
    }

    private IEnumerator UpdateSliderTimer(float time)
    {
        yield return new WaitForSeconds(time);
        ProgressUpdate.Invoke(CurrentBusiness.UpgradeProgress);
    }

    
    public void Show(Business business, PlayerStats playerStats, bool withAnimation = true)
    {
        CurrentBusiness = business;
        PlayerStats = playerStats;

        _characterIcon.sprite = _startCharacterSelectSprite;

        if(business.WorkingCharacter != null)
        {
            _characterIcon.sprite = business.WorkingCharacter.Image;
            var sprite = business.WorkingCharacter.GetCharacterFeatureSprite();
            OnHasWorkingCharacter.Invoke();
        }
        else
        {
            OnHasNotWorkingCharacter.Invoke();
        }

        _businessName.text = business.Name;
        
        UpdateData();
        
        if(withAnimation)
            OnShow.Invoke();
    }

    public void Hide() => OnHide.Invoke();
}