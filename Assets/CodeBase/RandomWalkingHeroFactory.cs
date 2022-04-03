using System.Collections.Generic;
using CodeBase.CharactersLogic;
using HeroLogic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace CodeBase
{
    public class RandomWalkingHeroFactory : IFactory<List<CharacterWalk>>
    {
        private Hero[] _randomCharacters;
        private MovePoint[] _spawnPoints; 
        private int _heroesCount;

        public RandomWalkingHeroFactory(Hero[] characters, MovePoint[] spawnPoints, int count)
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
                var randomSpawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
            }

            return heroes;
        }
    }
}