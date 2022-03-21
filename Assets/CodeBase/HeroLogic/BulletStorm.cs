using Assets.CodeBase;
using UnityEngine;
using Zenject;

namespace HeroLogic
{
    public class BulletStorm : SkillEffect
    {
        [SerializeField] private DamageHero _prefab;

        public override void Active()
        {
            Target.Hero.SpriteRenderer.color = new Color(0, 0, 0, 1);

            var bullet = Instantiate(_prefab, Target.Transform.position + new Vector3(0, 5, 0), Quaternion.identity, Target.Transform);

            bullet.Target = Target;
        }
    }
}