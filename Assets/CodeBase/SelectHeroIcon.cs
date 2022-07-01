using HeroLogic;
using TMPro;
using UltEvents;
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

        [Space]

        [SerializeField] private Image _featureImage;
        [SerializeField] private Sprite[] _features;

        [SerializeField] private GameObject[] _gameObjects;

        private SelectHeroInBusiness _menu;
        private Hero _hero;

        private bool _isPlayerHero;

        public void Render()
        {
            if(_isPlayerHero)
            {
                _nameText.text = "Unselect";

                foreach(var gameObject in _gameObjects)
                {
                    gameObject.SetActive(false);
                }

                _heroImage.sprite = _hero.Image;

                _featureImage.sprite = _features[Random.Range(0, _features.Length)];

                return;
            }

            _nameText.text = _hero.Name;

            foreach (var gameObject in _gameObjects)
            {
                gameObject.SetActive(true);
            }

            _heroImage.sprite = _hero.Image;

            _featureImage.sprite = _features[Random.Range(0, _features.Length)];
        }

        public void Select()
        {
            if (_isPlayerHero)
            {
                _menu.Select(null);

                _onSelected.Invoke();

                return;
            }

            _menu.Select(_hero);

            _onSelected.Invoke();
        }

        public void Hide()
        {
            _menu.Hide();
        }

        public void Initialize(Hero hero, SelectHeroInBusiness menu, bool isBusinessHero = false)
        {
            _hero = hero;
            _menu = menu;

            _isPlayerHero = isBusinessHero;

            _onInitialize.Invoke();
        }
    }
}