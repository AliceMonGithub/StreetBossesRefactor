using CodeBase;
using Factories;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.CodeBase.Zenject
{
    public class HeroPlacingIconsInstaller : MonoInstaller
    {
        [SerializeField] private PlayerStats _playerStats;
        
        [Space]

        [SerializeField] private HeroPlacingIcon _icon;
        [SerializeField] private HeroSelectMenu _selectMenu;
        [SerializeField] private Transform _grid;

        private HeroPlacingIconFactory _factory = new HeroPlacingIconFactory();

        public override void InstallBindings()
        {
            foreach(var hero in _playerStats.Heroes)
            {
                //if (hero.Energy == 0) continue;

                var icon = _factory.Create(_icon, _grid);

                icon.Initialize(hero, _selectMenu);
            }
        }
    }
}