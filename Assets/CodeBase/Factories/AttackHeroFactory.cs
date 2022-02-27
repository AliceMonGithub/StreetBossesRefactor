using HeroLogic;
using UnityEngine;
using Zenject;

namespace Factories
{
    public class AttackHeroFactory : IFactory<HeroAttack, Vector3, HeroAttack>
    {
        public HeroAttack Create(HeroAttack prefab, Vector3 position) => 
            Object.Instantiate(prefab, position, Quaternion.identity);
    }
}
