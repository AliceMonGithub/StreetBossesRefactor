using Assets.CodeBase;
using HeroLogic;
using System.Collections.Generic;
using System.Linq;
using UltEvents;
using UnityEngine;
using Zenject;

namespace CodeBase.UILogic
{
    public class SelectHeroInSecurity : SelectingMenu
    {
        [SerializeField] private UltEvent _onInitialized;
        [SerializeField] private UltEvent _onHide;

        [Space]

        [SerializeField] private PlayerStats _playerStats;

        [Space]

        [SerializeField] private BusinessUpgradeMenu _upgradeMenu;
        [SerializeField] private SelectSecurityIcon _icon;
        [SerializeField] private Transform _grid;

        private List<GameObject> _icons = new List<GameObject>();

        private PatrolHeroesInstaller _patrolHeroes;
        private Business _business;

        private int _index;

        [Inject]
        private void Construct(PatrolHeroesInstaller installer)
        {
            _patrolHeroes = installer;
        }

        public void Render()
        {
            _icons.ForEach(icon => Destroy(icon));

            _icons.Clear();

            foreach (var hero in _playerStats.Heroes.Value)
            {
                if (hero.Business != null) continue;

                if(hero.SecurityBusiness != null && hero.SecurityBusiness != _business)
                {
                    var Heroicon = Instantiate(_icon, _grid);

                    Heroicon.Initialize(hero, this, true);

                    _icons.Add(Heroicon.gameObject);

                    continue;
                }

                var icon = Instantiate(_icon, _grid);

                icon.Initialize(hero, this);

                _icons.Add(icon.gameObject);
            }
        }

        public void Initialize(Business business, int index)
        {
            _business = business;
            _index = index;

            _onInitialized.Invoke();
        }

        public void Hide()
        {
            _onHide.Invoke();
        }

        public override void Select(Hero hero)
        {
            if (hero == null)
            {
                var patrolHero = _patrolHeroes.SpawnedHeroes.FirstOrDefault(spawnedHero => spawnedHero.Name == hero.Name);

                if (patrolHero != null)
                {
                    patrolHero.gameObject.SetActive(true);
                }
            }
            else
            {
                var patrolHero = _patrolHeroes.SpawnedHeroes.FirstOrDefault(spawnedHero => spawnedHero.Name == hero.Name);

                if (patrolHero != null)
                {
                    patrolHero.gameObject.SetActive(false);
                }
            }

            _business.SetSecurity(hero, _index);

            _upgradeMenu.Render();
        }
    }
}