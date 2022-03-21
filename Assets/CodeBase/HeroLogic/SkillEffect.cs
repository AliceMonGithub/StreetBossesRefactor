using UnityEngine;

namespace HeroLogic
{
    public abstract class SkillEffect : MonoBehaviour
    {
        [Header("Skill properties")]
        [SerializeField] private Sprite _skillImage;

        public HeroAttack Hero { get; set; }
        public HeroAttack Target { get; set; }

        public Sprite SkillImage => _skillImage;

        public abstract void Active();
    }
}