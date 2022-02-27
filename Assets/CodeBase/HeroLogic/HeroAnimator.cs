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
            _animator.SetFloat(SpeedHash, _hero.Velosity);
        }

        public void TriggerAttack()
        {
            _animator.SetTrigger(AttackHash);
        }

        public void TriggerDie()
        {
            _animator.SetTrigger(DieHash);
        }
    }
}