using Factories;
using HeroLogic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeBase
{
    public class HeroSelectMenu : MonoBehaviour
    {
        [SerializeField] private List<SpawnPoint> _spawnPoints;

        private List<HeroAttack> _spawnedHeroes = new List<HeroAttack>();

        private AttackHeroFactory _factory = new AttackHeroFactory();

        public bool TrySelectHero(HeroAttack hero, HeroPlacingIcon sender)
        {
            var spawnPoint = _spawnPoints.Find(spawnPoint => spawnPoint.HaveEntity == false);

            if (spawnPoint != null)
            {
                var spawnedHero = _factory.Create(hero, spawnPoint.transform.position);

                _spawnedHeroes.Add(spawnedHero);

                spawnedHero.SpawnPoint = spawnPoint;

                sender.SpawnedHero = spawnedHero;

                spawnPoint.HaveEntity = true;

                return true;
            }

            return false;
        }

        public void DeselectHero(HeroAttack hero, HeroPlacingIcon sender)
        {
            var selectedHero = _spawnedHeroes.Find(spawnedHero => hero == spawnedHero);

            _spawnedHeroes.Remove(selectedHero);

            hero.SpawnPoint.HaveEntity = false;

            hero.SpawnPoint = null;

            sender.SpawnedHero = null;

            Destroy(selectedHero.gameObject);
        }
    }
}