using Assets.CodeBase;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Factories
{
    public class HeroPlacingIconFactory : IFactory<HeroPlacingIcon, Transform, HeroPlacingIcon>
    {
        public HeroPlacingIcon Create(HeroPlacingIcon icon, Transform parent) =>
            Object.Instantiate(icon, parent);
    }
}