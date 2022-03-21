using HeroLogic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.CodeBase.SkillMenu
{
    public class SkillIcon : MonoBehaviour
    {
        [SerializeField] private Image _image;

        private SkillBehavior _skillBehavior;
        private SkillEffect _skill;

        public void Active()
        {
            _skillBehavior.FindEnemy(_skill);

            Destroy(gameObject);
        }

        public void Render()
        {
            _image.sprite = _skill.SkillImage;
        }

        public void Initialize(SkillEffect skill, SkillBehavior skillBehavior)
        {
            _skillBehavior = skillBehavior;
            _skill = skill;

            Render();
        }
    }
}