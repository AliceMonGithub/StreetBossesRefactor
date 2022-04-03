using CodeBase;
using Factories;
using HeroLogic;
using System.Collections.Generic;
using UnityEngine;

namespace Zenject
{
    public class PatrolHeroesInstaller : MonoInstaller
    {
        [SerializeField] private PlayerStats _playerStats;
        [SerializeField] private MovePoint[] _movePoints;
        
        private PatrolHeroFactory _heroesFactory = new PatrolHeroFactory();

        private List<Hero> _spawnedHeroes = new List<Hero>();

        public List<Hero> SpawnedHeroes => _spawnedHeroes;

        public override void InstallBindings()
        {
            foreach (var hero in _playerStats.Heroes.Value)
            {
                var movePoint = GetRandomMovePoint();

                var spawnedHero = _heroesFactory.Create(hero.PatrolHero, movePoint.Transform.position);

                spawnedHero.Initialize(movePoint);

                _spawnedHeroes.Add(spawnedHero.Hero);
            }

            foreach (var hero in SpawnedHeroes)
            {
                if(hero.Business != null)
                {
                    hero.gameObject.SetActive(false);
                }
            }

            Container.Bind<PatrolHeroesInstaller>().FromInstance(this);
        }

        private MovePoint GetRandomMovePoint() =>
            _movePoints[Random.Range(0, _movePoints.Length)];
    }
}