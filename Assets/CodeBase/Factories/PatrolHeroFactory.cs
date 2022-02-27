using HeroLogic;
using UnityEngine;
using Zenject;

namespace Factories
{
    public class PatrolHeroFactory : IFactory<PatrolMovement, Vector3, PatrolMovement>
    {
        public PatrolMovement Create(PatrolMovement hero, Vector3 position) =>
            Object.Instantiate(hero, position, Quaternion.identity);
    }
}
