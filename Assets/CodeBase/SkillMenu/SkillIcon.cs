using HeroLogic;
using System.Collections;
using UltEvents;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.CodeBase.SkillMenu
{
    public class SkillIcon : MonoBehaviour
    {
        [SerializeField] private Image _heroImage;
        [SerializeField] private Image _skillImage;

        private SkillBehavior _skillBehavior;

        private SkillEffect _skill;
        private Hero _hero;

        public string HeroName => _hero.Name;

        private void Awake()
        {
            _skillBehavior = FindObjectOfType<SkillBehavior>();
        }

        public void Click()
        {
            _skillBehavior.ActiveSkill(_skill, _hero);

            Destroy(gameObject);
        }

        public void Render()
        {
            _heroImage.sprite = _hero.Image;
            _skillImage.sprite = _skill.Image;
        }

        public void Initialize(Hero hero)
        {
            _skill = hero.Skill;
            _hero = hero;

            Render();
        }
    }
}