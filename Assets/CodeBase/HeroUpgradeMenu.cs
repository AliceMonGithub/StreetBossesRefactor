using HeroLogic;
using TMPro;
using UltEvents;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase
{
    public class HeroUpgradeMenu : MonoBehaviour
    {
        [SerializeField] private UltEvent _onBuy;
        [SerializeField] private UltEvent _onUpgrade;

        [Space]

        [SerializeField] private Sprite[] _flagSprites;

        [Space]

        [SerializeField] private UltEvent _onInitialize;

        [SerializeField] private PlayerStats _playerStats;

        [SerializeField] private Slider _upgradeProgressSlider;

        [SerializeField] private Image _heroImage;
        [SerializeField] private Image _familyImage;

        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _healthText;
        [SerializeField] private TMP_Text _damageText;
        [SerializeField] private TMP_Text _costText;

        [SerializeField] private TMP_Text _energyText;

        [SerializeField] private TMP_Text _timeText;

        private Hero _hero;

        private void Update()
        {
            UpdateDynamic();
        }

        public void Render()
        {
            _upgradeProgressSlider.maxValue = 75;
            _upgradeProgressSlider.value = _hero.UpgradeProgress;

            _heroImage.sprite = _hero.Image;

            _nameText.text = _hero.Name;
            _healthText.text = _hero.HeroAttack.Health.ToString();
            _damageText.text = _hero.HeroAttack.Damage.ToString();
            _energyText.text = _hero.Energy.ToString();
            _costText.text = ((int)(_hero.UpgradeCost * 0.25f)).ToString() + "$";

            switch (_hero.FamilyType)
            {
                case FamilyType.Russian:
                    _familyImage.sprite = _flagSprites[0];
                    break;
                case FamilyType.Italian:
                    _familyImage.sprite = _flagSprites[1];
                    break;
                case FamilyType.Japanese:
                    _familyImage.sprite = _flagSprites[2];
                    break;
                case FamilyType.Jamaican:
                    _familyImage.sprite = _flagSprites[3];
                    break;
            }
        }

        private void UpdateDynamic()
        {
            _timeText.text = ((int)_hero.CurrentEnergyTime).ToString();
        }

        public void TryBuyUpgrade()
        {
            if(_hero.Level != _hero.LevelsProperties.Length + 1 && _hero.UpgradeCost * 0.25f <= _playerStats.Money.Value)
            {
                _playerStats.Money.Value -= (int)Mathf.Round(_hero.UpgradeCost * 0.25f);

                _hero.TryUpgrade();

                _onBuy.Invoke();
            }
        }

        public void Initialize(Hero hero)
        {
            _hero = hero;

            _onInitialize.Invoke();
        }
    }
}