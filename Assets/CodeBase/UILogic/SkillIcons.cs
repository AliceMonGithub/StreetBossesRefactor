using Assets.CodeBase.SkillMenu;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.CodeBase.UILogic
{
    public class SkillIcons : MonoBehaviour
    {
        [SerializeField] private SkillIcon _skillIcon;
        [SerializeField] private Transform _parent;

        [SerializeField] private AttackHeroes _heroesData;
        [SerializeField] private SkillBehavior _skillBehavior;

        private List<SkillIcon> _icons = new List<SkillIcon>();

        public void DestroyIcon(string name)
        {
            var icon = _icons.FirstOrDefault(icon => icon?.HeroName == name);

            if (icon == null) return;

            Destroy(icon.gameObject);
        }

        public void Render()
        {
            _heroesData.PlayerHeroes.ForEach(hero =>
            {
                if (hero.Skill != null)
                {
                    var icon = Instantiate(_skillIcon, _parent);

                    icon.Initialize(hero);

                    _icons.Add(icon);
                }
            });
        }
    }
}