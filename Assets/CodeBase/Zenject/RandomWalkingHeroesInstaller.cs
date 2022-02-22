using UnityEngine;
using Zenject;

namespace CodeBase.Zenject
{
    public class RandomWalkingHeroesInstaller : MonoInstaller
    {
        [SerializeField] private Character[] _characters;
        [SerializeField] private MovePoint[] _movePoints;
        [SerializeField] private int _charactersCount;
        
        private RandomWalkingHeroFactory _heroesFactory;
        
        public override void InstallBindings()
        {
            _heroesFactory = new RandomWalkingHeroFactory(_characters, _movePoints, _charactersCount);

            _heroesFactory.Create();
        }
    }
}