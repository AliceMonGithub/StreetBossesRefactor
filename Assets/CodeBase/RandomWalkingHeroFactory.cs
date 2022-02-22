using System.Collections.Generic;
using CodeBase.CharactersLogic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace CodeBase
{
    public class RandomWalkingHeroFactory : IFactory<List<CharacterWalk>>
    {
        private Character[] _randomCharacters;
        private MovePoint[] _spawnPoints; 
        private int _heroesCount;

        public RandomWalkingHeroFactory(Character[] characters, MovePoint[] spawnPoints, int count)
        {
            _spawnPoints = spawnPoints;
            _randomCharacters = characters;
            _heroesCount = count;
        }

        public List<CharacterWalk> Create()
        {
            var heroes = new List<CharacterWalk>();
            
            for (int i = 0; i < _heroesCount; i++)
            {
                var spawnHero = _randomCharacters[i].CharacterWalk;
                var randomSpawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];

                var hero = Object.Instantiate(spawnHero, randomSpawnPoint.Transform.position, Quaternion.identity);
                
                hero.Initialize(randomSpawnPoint.Points);
                
                heroes.Add(hero);
            }

            return heroes;
        }
    }
}