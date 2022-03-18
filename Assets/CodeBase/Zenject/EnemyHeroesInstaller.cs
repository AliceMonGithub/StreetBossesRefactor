﻿using Factories;
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

        [Space]

        [SerializeField] private AttackHeroes _attackHeroes;

        private EnemyHeroFactory _factory = new EnemyHeroFactory();

        public override void InstallBindings()
        {
            var heroes = _playerStats.AttackingBusiness.EnemyHeroes;

            for (int i = 0; i < heroes.Length; i++)
            {
                var spawnedHero = _factory.Create(heroes[i], _spawnPoints[i].position);

                spawnedHero.Hero.IsPlayerHero = false;

                spawnedHero.Transform.localScale = new Vector3(1, 1, 1);

                _attackHeroes.EnemyHeroes.Add(spawnedHero.Hero);
            }
        }
    }
}