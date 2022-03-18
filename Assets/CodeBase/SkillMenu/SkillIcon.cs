using HeroLogic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.CodeBase.SkillMenu
{
    public class SkillIcon : MonoBehaviour
    {
        [SerializeField] private Image _image;

        [Space]

        [SerializeField] private Image _heroImage;

        private SkillEffect _skill;
        private HeroAttack _hero;

        public void Update()
        {
            if(_hero.Hero.SkillReloading)
            {
                _image.fillAmount = Mathf.Clamp(_hero.Hero.CurrentTime / (_hero.Hero.SkillReloadingTime / 100f) / 100f, 0f, 1f) + 0.05f;
            }
        }

        public void Active()
        {
            if (_hero.Hero.SkillReloading) return;

            _skill.Active();

            _hero.Hero.ReloadSkill();
        }

        public void Render()
        {
            _heroImage.sprite = _skill.SkillImage;

            _heroImage.enabled = true;
        }

        public void Initialize(SkillEffect skill, HeroAttack hero)
        {
            _skill = skill;
            _hero = hero;

            Render();
        }
    }
}