using HeroLogic;
using System.Collections;
using TMPro;
using UltEvents;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.CodeBase
{
    public class HeroUpgradeIcon : MonoBehaviour
    {
        [SerializeField] private UltEvent _onClick;
        [SerializeField] private UltEvent _onInitialize;

        [SerializeField] private Image _heroIcon;

        [Space]

        [SerializeField] private TMP_Text _currentLevelText;
        [SerializeField] private TMP_Text _maxLevelText;

        private HeroesUpgradeMenu _upgradeMenu;
        private Hero _hero;

        public void SelectHero()
        {
            _upgradeMenu.SelectHero(_hero);
        }

        public void OnClick()
        {
            _onClick.Invoke();
        }

        public void Render()
        {
            _heroIcon.sprite = _hero.Image;

            _currentLevelText.text = _hero.Level.ToString();
            _maxLevelText.text = (_hero.LevelsProperties.Length + 1).ToString();
        }

        public void Initialize(Hero hero, HeroesUpgradeMenu upgradeMenu)
        {
            _upgradeMenu = upgradeMenu;
            _hero = hero;

            _onInitialize.Invoke();
        }
    }
}