using Assets.CodeBase;
using UnityEngine;
using Zenject;

namespace Factories
{
    public class HeroUpgradeIconFactory : IFactory<HeroUpgradeIcon, Transform, HeroUpgradeIcon>
    {
        public HeroUpgradeIcon Create(HeroUpgradeIcon iconPrefab, Transform grid) =>
            Object.Instantiate(iconPrefab, grid);
    }
}
