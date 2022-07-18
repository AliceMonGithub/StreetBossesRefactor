using HeroLogic;
using TMPro;
using UltEvents;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UILogic
{
    public class HeroBuyIcon : MonoBehaviour
    {
        [SerializeField] private UltEvent _onBuy;

        [SerializeField] private PlayerStats _playerStats;

        [Space]

        [SerializeField] private Image[] _heroImages;

        [Space]

        [SerializeField] private TMP_Text _heroName;
        [SerializeField] private TMP_Text _heroCost;

        private HeroesBuyMenu _heroesBuyMenu;

        private Hero _hero;

        public void Initialize(Hero hero, HeroesBuyMenu heroesBuyMenu)
        {
            _hero = hero;
            _heroesBuyMenu = heroesBuyMenu;

            Render();
        }

        public void Buy()
        {
            if(_playerStats.Money.Value >= _hero.Cost)
            {

                _hero.SetRandomFamily();

                _playerStats.Heroes.Value.Add(_hero);
                
                _playerStats.Money.Value -= _hero.Cost;

                _heroesBuyMenu.PlayBuySound();
                
                _onBuy.Invoke();
            }
        }

        private void Render()
        {
            foreach (var heroImage in _heroImages)
            {
                heroImage.sprite = _hero.Image;
            }

            _heroName.text = _hero.Name;
            _heroCost.text = _hero.Cost.ToString();
        }
    }
}