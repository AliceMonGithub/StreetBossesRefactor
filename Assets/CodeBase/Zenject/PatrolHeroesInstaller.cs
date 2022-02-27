using CodeBase;
using Factories;
using UnityEngine;

namespace Zenject
{
    public class PatrolHeroesInstaller : MonoInstaller
    {
        [SerializeField] private PlayerStats _playerStats;
        [SerializeField] private MovePoint[] _movePoints;
        
        private PatrolHeroFactory _heroesFactory = new PatrolHeroFactory();
        
        public override void InstallBindings()
        {
            foreach (var hero in _playerStats.Heroes)
            {
                var movePoint = GetRandomMovePoint();

                var spawnedHero = _heroesFactory.Create(hero.PatrolHero, movePoint.Transform.position);

                spawnedHero.Initialize(movePoint);
            }
        }

        private MovePoint GetRandomMovePoint() =>
            _movePoints[Random.Range(0, _movePoints.Length)];
    }
}