using CodeBase;
using Factories;
using HeroLogic;
using System.Collections;
using System.Collections.Generic;
using UltEvents;
using UnityEngine;

namespace Assets.CodeBase
{
    public class HeroesUpgradeMenu : MonoBehaviour
    {
        [SerializeField] private UltEvent _onShow;

        [SerializeField] private HeroUpgradeIcon _icon;
        [SerializeField] private HeroUpgradeMenu _upgradeMenu;

        [SerializeField] private Transform _grid;

        [SerializeField] private PlayerStats _playerStats;

        private List<HeroUpgradeIcon> _icons = new List<HeroUpgradeIcon>();

        private HeroUpgradeIconFactory _factory = new HeroUpgradeIconFactory();

        public void SelectHero(Hero hero)
        {
            _upgradeMenu.Initialize(hero);
        }

        public void RenderHeroes()
        {
            _icons.ForEach(icon => Destroy(icon.gameObject));
            _icons.Clear();

            foreach(var hero in _playerStats.Heroes.Value)
            {
                var icon = _factory.Create(_icon, _grid);

                icon.Initialize(hero, this);

                _icons.Add(icon);
            }
        }

        public void Show()
        {
            _onShow.Invoke();
        }
    }
}