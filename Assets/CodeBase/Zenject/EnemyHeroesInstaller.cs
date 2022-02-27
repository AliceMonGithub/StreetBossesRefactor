using Factories;
using CodeBase;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.CodeBase.Zenject
{
    public class EnemyHeroesInstaller : MonoInstaller
    {
        [SerializeField] private Transform[] _spawnPoints;

        [SerializeField] private PlayerStats _playerStats;

        private EnemyHeroFactory _factory = new EnemyHeroFactory();

        public override void InstallBindings()
        {
            var heroes = _playerStats.AttackingBusiness.EnemyHeroes;

            for (int i = 0; i < heroes.Length; i++)
            {
                _factory.Create(heroes[i], _spawnPoints[i].position);
            }
        }
    }
}