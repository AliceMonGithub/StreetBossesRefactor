using Assets.CodeBase.SkillMenu;
using HeroLogic;
using UltEvents;
using UnityEngine;

namespace Assets.CodeBase
{
    public class SelectEnemyMenu : MonoBehaviour
    {
        [SerializeField] private UltEvent _showEvent;
        [SerializeField] private UltEvent _hideEvent;

        [SerializeField] private SkillIcon _prefab;
        [SerializeField] private Transform _grid;

        private SkillBehavior _skillBehavior;
        private Hero _hero;

        public void RenderIcons()
        {
            var icon = Instantiate(_prefab, _grid);

            icon.Initialize(_hero.Skill, _skillBehavior);
        }

        public void Initialize(Hero hero, SkillBehavior skillBehavior)
        {
            _skillBehavior = skillBehavior;
            _hero = hero;

            RenderIcons();

            _showEvent.Invoke();
        }

        public void Hide()
        {
            _hideEvent.Invoke();
        }
    }
}