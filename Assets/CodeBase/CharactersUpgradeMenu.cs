using System;
using CodeBase.UILogic;
using UltEvents;
using UnityEngine;

namespace CodeBase
{
    public class CharactersUpgradeMenu : MonoBehaviour
    {
        [SerializeField] private PlayerStats _playerStats;
        [SerializeField] private CharacterUpgradeIcon _iconPrefab;
        [SerializeField] private HeroUpgradeMenu heroesPanel;
        [SerializeField] private Transform _grid;

        [SerializeField] private UltEvent OnShow;
        [SerializeField] private UltEvent OffShow;

        public void Show()
        {
            Render();
            OnShow.Invoke();
        }

        public void Render()
        {
            foreach (Transform child in _grid)
            {
                Destroy(child.gameObject);
            }
            _playerStats.Characters.ForEach(character => Instantiate(_iconPrefab, _grid).Initialize(character, heroesPanel, this));
        }

        public void Hide() => OffShow.Invoke();
    }
}