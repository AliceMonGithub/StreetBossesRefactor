using Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UltEvents;
using UnityEngine;

namespace CodeBase.Mao_AddiotionUiScript
{
    public class ChestBust : MonoBehaviour
    {
        [SerializeField] private TMP_Text _costText;

        public PlayerStats PlayerStats;

        private Booster _booster;

        private List<Character> _characters;

        public event Action<ChestBust> Opening;
        public UltEvent OnOpen;

        public Color[] BackgroundColors;

        public LootOfChest TemplateLoot;
        public List<LootOfChest> LootMenues;

        public void Open()
        {
            if (PlayerStats.Money < _booster.Cost) return;

            PlayerStats.Money -= _booster.Cost;

            foreach(var character in _characters)
            {
                PlayerStats.Characters.Add(character);

                TemplateLoot.BackgroundColor = BackgroundColors[UnityEngine.Random.Range(0, BackgroundColors.Length)];
                TemplateLoot.CharacterImage.sprite = character.Image;
                TemplateLoot.CharacterName.text = character.Name;

                LootMenues.Add(TemplateLoot);
            }

            foreach(var menu in LootMenues)
            {
                print(menu.CharacterName.text);
            }

            Opening?.Invoke(this);
            OnOpen.Invoke();
        }

        public List<Character> GetLoot()
        {
            return _characters;
        }

        public void Initialize(Booster booster, PlayerStats playerStats)
        {
            _booster = booster;

            _costText.text = _booster.Cost.ToString();
            _characters = booster.Characters;

            PlayerStats = playerStats;
        }
    }
}