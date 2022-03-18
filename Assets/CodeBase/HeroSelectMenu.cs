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

        [SerializeField] private AttackHeroes _attackHeroes;

        private List<HeroAttack> _spawnedHeroes = new List<HeroAttack>();

        private AttackHeroFactory _factory = new AttackHeroFactory();

        public bool TrySelectHero(HeroAttack hero, HeroPlacingIcon sender)
        {
            var spawnPoint = _spawnPoints.Find(spawnPoint => spawnPoint.HaveEntity == false);

            if (spawnPoint != null)
            {
                var spawnedHero = _factory.Create(hero, spawnPoint.transform.position);

                spawnedHero.Hero.IsPlayerHero = true;

                _spawnedHeroes.Add(spawnedHero);

                _attackHeroes.PlayerHeroes.Add(spawnedHero.Hero);

                var scale = spawnedHero.Transform.localScale;

                scale.x = -Mathf.Abs(scale.x);

                spawnedHero.Transform.localScale = scale; 

                spawnedHero.SpawnPoint = spawnPoint;

                sender.SpawnedHero = spawnedHero;

                spawnPoint.HaveEntity = true;

                spawnedHero.Hero.HealthbarText.localScale = new Vector3(-1, 1, 1);

                return true;
            }

            return false;
        }

        public void DeselectHero(HeroAttack hero, HeroPlacingIcon sender)
        {
            var selectedHero = _spawnedHeroes.Find(spawnedHero => hero == spawnedHero);

            _spawnedHeroes.Remove(selectedHero);

            _attackHeroes.PlayerHeroes.Remove(selectedHero.Hero);

            hero.SpawnPoint.HaveEntity = false;

            hero.SpawnPoint = null;

            sender.SpawnedHero = null;

            Destroy(selectedHero.gameObject);
        }
    }
}