using System.Collections;
using UnityEngine;

namespace HeroLogic
{
    public class HeroAnimator : MonoBehaviour
    {
        private int SpeedHash = Animator.StringToHash("Speed");
        private int AttackHash = Animator.StringToHash("Attack");
        private int DieHash= Animator.StringToHash("Die");

        [SerializeField] private Hero _hero;
        [SerializeField] private Animator _animator;

        private void Update()
        {
            _animator.SetFloat("Speed", _hero.Velosity);
        }

        public void TriggerAttack()
        {
            _animator.SetTrigger("Attack");
        }

        public void TriggerDie()
        {
            if (_hero.Dead) return;

            _animator.SetTrigger("Die");
        }
    }
}