using HeroLogic;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.UILogic
{
    public class HeroesBuyMenu : MonoBehaviour
    {
        [SerializeField] private PlayerStats _playerStats;

        [Space]

        [SerializeField] private List<Hero> _heroes;

        [Space]

        [SerializeField] private HeroBuyIcon _icon;

        [Space]

        [SerializeField] private Transform _grid;

        [SerializeField] private AudioSource _buySound;

        private List<GameObject> _icons = new List<GameObject>();

        private void OnEnable()
        {
            RenderIcons();
        }

        private void OnDisable()
        {
            _icons.ForEach(icon => Destroy(icon));

            _icons.Clear();
        }

        private void RenderIcons()
        {
            foreach (var hero in _heroes)
            {
                if (_playerStats.Heroes.Value.Any(playerHero => playerHero == hero)) continue;

                var spawnedIcon = Instantiate(_icon, _grid);

                spawnedIcon.Initialize(hero, this);

                _icons.Add(spawnedIcon.gameObject);
            }
        }

        public void PlayBuySound()
        {
            _buySound.Play();
        }
    }
}