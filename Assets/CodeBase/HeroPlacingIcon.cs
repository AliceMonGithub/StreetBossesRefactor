using HeroLogic;
using System.Collections;
using TMPro;
using UltEvents;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.CodeBase
{
    public class HeroPlacingIcon : MonoBehaviour
    {
        [SerializeField] private UltEvent _onInitialize;
        [SerializeField] private UltEvent _onSelect;
        [SerializeField] private UltEvent _onDeselect;

        [SerializeField] private Image _heroIcon;

        [Space]

        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _levelText;
        [SerializeField] private TMP_Text _healthText;
        [SerializeField] private TMP_Text _damageText;
        [SerializeField] private TMP_Text _familyTypeText;

        private HeroSelectMenu _selectMenu;
        private Hero _hero;

        private bool _selected;

        public HeroAttack SpawnedHero { get; set; }

        public void TrySelect()
        {
            if(_selected)
            {
                Deselect();
            }
            else
            {
                Select();
            }
        }

        public void Select()
        {
            if (_selectMenu.TrySelectHero(_hero.HeroAttack, this))
            {
                _selected = true;

                _onSelect.Invoke();
            }
        }

        public void Deselect()
        {
            _selectMenu.DeselectHero(SpawnedHero, this);

            _selected = false;

            _onDeselect.Invoke();
        }

        public void Render()
        {
            _heroIcon.sprite = _hero.Image;

            _nameText.text = _hero.Name;

            _levelText.text = _hero.Level.ToString();
            _healthText.text = _hero.HeroAttack.Health.ToString();
            _damageText.text = _hero.HeroAttack.Damage.ToString();
            _familyTypeText.text = _hero.FamilyType.ToString();
        }

        public void Initialize(Hero hero, HeroSelectMenu selectMenu)
        {
            _selectMenu = selectMenu;
            _hero = hero;

            _onInitialize.Invoke();
        }
    }
}