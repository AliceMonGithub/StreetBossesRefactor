using HeroLogic;
using System.Collections;
using TMPro;
using UltEvents;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.CodeBase
{
    public class SelectHeroIcon : MonoBehaviour
    {
        [SerializeField] private UltEvent _onInitialize;
        [SerializeField] private UltEvent _onSelected;

        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private Image _heroImage;

        private SelectHeroInBusiness _menu;
        private Hero _hero;

        public void Render()
        {
            _nameText.text = _hero.Name;

            _heroImage.sprite = _hero.Image;
        }

        public void Select()
        {
            _menu.Select(_hero);

            _onSelected.Invoke();
        }

        public void Hide()
        {
            _menu.Hide();
        }

        public void Initialize(Hero hero, SelectHeroInBusiness menu)
        {
            _hero = hero;
            _menu = menu;

            _onInitialize.Invoke();
        }
    }
}