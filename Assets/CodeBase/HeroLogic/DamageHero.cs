using UnityEngine;

namespace HeroLogic
{
    public class DamageHero : MonoBehaviour
    {
        [SerializeField] private int _damage;

        public HeroAttack Target;

        public void Attack()
        {
            Target.DecreaseHealth(_damage);
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}