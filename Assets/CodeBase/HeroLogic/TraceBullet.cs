using HeroLogic;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.CodeBase.HeroLogic
{
    public class TraceBullet : SkillEffect
    {
        [SerializeField] private Bullet _bullet;

        public override void Active()
        {
            Target.Hero.SpriteRenderer.color = new Color(0, 0, 0, 1);

            StartCoroutine(CreateBullets());
        }

        public IEnumerator CreateBullets()
        {
            for (int i = 0; i < 3; i++)
            {
                var bullet = Instantiate(_bullet, transform.position + Vector3.up * 0.5f, Quaternion.Euler(0, 0, -90));

                bullet.Target = Target;

                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}