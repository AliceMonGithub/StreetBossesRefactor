using CodeBase;
using HeroLogic;
using System.Collections;
using System.Collections.Generic;
using UltEvents;
using UniRx;
using UnityEngine;

namespace Assets.CodeBase
{
    public class SelectHeroInBusiness : SelectingMenu
    {
        [SerializeField] private UltEvent _onInitialized;

        [SerializeField] private PlayerStats _playerStats;
        [SerializeField] private SelectHeroIcon _icon;
        [SerializeField] private BusinessUpgradeMenu _upgradeMenu;
        [SerializeField] private Transform _grid;

        [SerializeField] private SelectHeroIcon _nullIcon;

        private List<GameObject> _icons = new List<GameObject>();

        private Business _business;

        public void Render()
        {
            _icons.ForEach(icon => Destroy(icon));

            _icons.Clear();

            var nullIcon = Instantiate(_nullIcon, _grid);

            nullIcon.Initialize(null, this);

            _icons.Add(nullIcon.gameObject);

            foreach(var hero in _playerStats.Heroes)
            {
                if (hero.Business) continue;

                var icon = Instantiate(_icon, _grid);

                icon.Initialize(hero, this);

                _icons.Add(icon.gameObject);
            }
        }

        public override void Select(Hero hero)
        {
            _business.SetWorkingHero(hero);

            _upgradeMenu.Render();
        }

        public void Initialize(Business business)
        {
            _business = business;

            _onInitialized.Invoke();
        }
    }

    public abstract class SelectingMenu : MonoBehaviour
    {
        public Hero Hero { get; set; }

        public abstract void Select(Hero hero);
    }
}