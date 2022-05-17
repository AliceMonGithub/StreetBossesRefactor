using CodeBase;
using HeroLogic;
using System.Collections;
using UltEvents;
using UnityEngine;
using Zenject;

namespace Assets.CodeBase.SkillMenu
{
    public class SkillsMenu : MonoBehaviour
    {
        [SerializeField] private UltEvent _onShow;

        [SerializeField] private AttackHeroes _heroes;

        [Space]

        [SerializeField] private SkillIcon _prefab;

        [SerializeField] private Transform _grid;

        [Inject]
        private void Construct(AttackHeroes heroes)
        {
            _heroes = heroes;
        }

        private void OnEnable()
        {
            _onShow.Invoke();
        }

        public void Render()
        {
            var heroes = _heroes.PlayerHeroes.FindAll(hero => hero.Skill != null);

            heroes.ForEach(hero => InstantiateIcon(hero.Skill, hero));
        }

        public void InstantiateIcon(SkillEffect skill, Hero hero)//, AttackHeroes attackHeroes)
        {
            var icon = Instantiate(_prefab, _grid);
        }
    }
}