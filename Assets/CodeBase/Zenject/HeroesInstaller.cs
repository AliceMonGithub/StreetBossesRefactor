using Assets.CodeBase;
using System.Collections;
using UnityEngine;

namespace Zenject
{
    public class HeroesInstaller : MonoInstaller
    {
        [SerializeField] private AttackHeroes _heroes;

        public override void InstallBindings()
        {
            Container.Bind<AttackHeroes>().FromInstance(_heroes).AsCached();
        }
    }
}