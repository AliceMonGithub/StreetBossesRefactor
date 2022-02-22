using Lean.Transition;
using TMPro;
using UltEvents;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase
{
    public class HeroUpgradeMenu : MonoBehaviour
    {
        [SerializeField] private PlayerStats _playerStats;
        
        [Space]
        
        [SerializeField] private Slider _levelSlider;
        [SerializeField] private Slider _upgradeSlider;

        [Space]
        
        [SerializeField] private Image _heroImage;
        
        [Space]
        
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _healthText;
        [SerializeField] private TMP_Text _damageText;
        [SerializeField] private TMP_Text _levelText;
        [SerializeField] private TMP_Text _costText;
        
        private Character _character;
        private CharactersUpgradeMenu _charactersMenu;
        [SerializeField] private UltEvent _onShow;
        [SerializeField] private UltEvent _onHide;

        public UltEvent<int> LevelSliderUpdate;
        public UltEvent<float> UpgradeSliderUpdate;

        public void Upgrade()
        {
            var cost = Mathf.RoundToInt(_character.Upgrades[_character.Level].UpgradeCost * 0.25f);
            
            if (_character.Upgrades.Length > _character.Level && _character.UpgradePercent != 100 && _playerStats.Money >= cost)
            {
                _character.UpgradePercent += 25;

                _playerStats.Money -= cost;
            }

            if (_character.UpgradePercent >= 100)
            {
                _character.Level++;
                
                var currentUpgrade = _character.Upgrades[_character.Level - 1];

                _character.Health = currentUpgrade.HealthUpgrade;
                _character.Damage = currentUpgrade.DamageUpgrade;
                _character.MoveSpeed = currentUpgrade.SpeedUpgrade;
                
                _character.UpgradePercent = 0;
            }
            
            Render(TypeUpdateSlider.UltEvent);
        }
        
        public void Show(Character character, CharactersUpgradeMenu charactersMenu)
        {
            _character = character;
            _charactersMenu = charactersMenu;
            
            Render(TypeUpdateSlider.Hard);
            
            _onShow.Invoke();
        }

        private void Render(TypeUpdateSlider t)
        {
            _charactersMenu.Render();
            
            _levelSlider.maxValue = _character.Upgrades.Length+1;
            if(t== TypeUpdateSlider.Hard)
                _levelSlider.value = _character.Level + 1;
            else 
                LevelSliderUpdate.Invoke(_character.Level + 1);

            if(t == TypeUpdateSlider.Hard)
                _upgradeSlider.value = _character.UpgradePercent;
            else
                UpgradeSliderUpdate.Invoke(_character.UpgradePercent);

            _heroImage.sprite = _character.Image;

            _nameText.text = _character.Name;
            _healthText.text = _character.Health.ToString();
            _damageText.text = _character.Damage.ToString();
            _levelText.text = (_character.Level + 1) + " / " + (_character.Upgrades.Length + 1);

            if (_character.Upgrades.Length > _character.Level)
            {
                _costText.text = (_character.Upgrades[_character.Level].UpgradeCost * 0.25f).ToString();   
            }
            else
            {
                _costText.text = "MAX";
            }
        }

        public void Hide()
        {
            _onHide.Invoke();
        }

        private enum TypeUpdateSlider
        {
            Hard, UltEvent
        }
    }
}