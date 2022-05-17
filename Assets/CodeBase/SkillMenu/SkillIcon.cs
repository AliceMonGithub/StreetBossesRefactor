using HeroLogic;
using System.Collections;
using UltEvents;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.CodeBase.SkillMenu
{
    public class SkillIcon : MonoBehaviour
    {
        [SerializeField] private UltEvent _onShow;

        [SerializeField] private Image _image;

        [SerializeField] private GameObject _tringle;

        private SkillBehavior _skillBehavior;
        private SkillEffect _skill;

        public GameObject Tringle => _tringle;

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

            _onShow.Invoke();
        }
    }
}