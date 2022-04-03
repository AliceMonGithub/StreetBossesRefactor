using CodeBase;
using HeroLogic;
using System.Collections;
using System.Collections.Generic;
using UltEvents;
using UniRx;
using UnityEngine;
using Zenject;

namespace Assets.CodeBase
{
    public class SelectHeroInBusiness : SelectingMenu
    {
        [SerializeField] private UltEvent _onInitialized;
        [SerializeField] private UltEvent _onHide;

        [SerializeField] private PlayerStats _playerStats;
        [SerializeField] private SelectHeroIcon _icon;
        [SerializeField] private BusinessUpgradeMenu _upgradeMenu;
        [SerializeField] private Transform _grid;

        [SerializeField] private SelectHeroIcon _nullIcon;

        private PatrolHeroesInstaller _patrolHeroes;

        private List<GameObject> _icons = new List<GameObject>();

        private Business _business;

        [Inject]
        private void Construct(PatrolHeroesInstaller installer)
        {
            _patrolHeroes = installer;
        }

        public void Hide()
        {
            _onHide.Invoke();
        }

        public void Render()
        {
            _icons.ForEach(icon => Destroy(icon));

            _icons.Clear();

            var nullIcon = Instantiate(_nullIcon, _grid);

            nullIcon.Initialize(null, this);

            _icons.Add(nullIcon.gameObject);

            foreach(var hero in _playerStats.Heroes.Value)
            {
                if (hero.Business) continue;

                var icon = Instantiate(_icon, _grid);

                icon.Initialize(hero, this);

                _icons.Add(icon.gameObject);
            }
        }
         
        public override void Select(Hero hero)
        {
            if(hero == null)
            {
                _patrolHeroes.SpawnedHeroes.Find(spawnedHero => spawnedHero.Name == _business.WorkingHero.Name).gameObject.SetActive(true);
            }
            else
            {
                _patrolHeroes.SpawnedHeroes.Find(spawnedHero => spawnedHero.Name == hero.Name).gameObject.SetActive(false);
            }

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