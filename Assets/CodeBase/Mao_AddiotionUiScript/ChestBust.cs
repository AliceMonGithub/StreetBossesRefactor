using Assets;
using HeroLogic;
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
        public event Action<ChestBust> Opening;
        public UltEvent OnOpen;

        [SerializeField] private TMP_Text _costText;

        public PlayerStats PlayerStats;

        private Booster _booster;

        private List<Hero> _heroes;

        public Color[] BackgroundColors;

        public LootOfChest TemplateLoot;
        public List<LootOfChest> LootMenues;

        public void Open()
        {
            if (PlayerStats.Money.Value < _booster.Cost) return;

            PlayerStats.Money.Value -= _booster.Cost;

            foreach (var hero in _heroes)
            {
                PlayerStats.Heroes.Add(hero);

                TemplateLoot.BackgroundColor = BackgroundColors[UnityEngine.Random.Range(0, BackgroundColors.Length)];
                TemplateLoot.CharacterImage.sprite = hero.Image;
                TemplateLoot.CharacterName.text = hero.Name;

                LootMenues.Add(TemplateLoot);
            }

            foreach (var menu in LootMenues)
            {
                print(menu.CharacterName.text);
            }

            Opening?.Invoke(this);
            OnOpen.Invoke();
        }

        public List<Hero> GetLoot()
        {
            return _heroes;
        }

        public void Initialize(Booster booster, PlayerStats playerStats)
        {
            _booster = booster;

            _costText.text = _booster.Cost.ToString();
            _heroes = booster.Heroes;

            PlayerStats = playerStats;
        }
    }
}