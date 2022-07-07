using UnityEngine;

namespace HeroLogic
{
    public abstract class SkillEffect : MonoBehaviour
    {
        [Header("Skill properties")]
        [SerializeField] private Sprite _image;

        public HeroAttack Hero { get; set; }
        public HeroAttack Target { get; set; }

        public Sprite Image => _image;

        public abstract void Active();
    }
}