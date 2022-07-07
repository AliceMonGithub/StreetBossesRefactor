using HeroLogic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using CodeBase;
using UnityEngine.SceneManagement;
using System.Linq;
using CodeBase.BotLogic;

namespace Assets.CodeBase
{
    public class AttackHeroes : MonoBehaviour
    {
        [SerializeField] private PlayerStats _playerStats;

        [SerializeField] private FinishMenu _winMenu;
        [SerializeField] private FinishMenu _loseMenu;

        [SerializeField] private GameObject[] _activatingMenues;
        [SerializeField] private GameObject[] _deactiveMenues;

        [SerializeField] private GameObject _tringle;

        public List<Hero> PlayerHeroes;
        public List<Hero> EnemyHeroes;


        private CompositeDisposable _disposable = new CompositeDisposable();

        private BotBootstrapper _botBootstrapper;

        private int _playerHeroesDied = 1;

        private bool _playing;

        public bool GameStarter { get; private set; }

        private void Awake()
        {
             _botBootstrapper = FindObjectOfType<BotBootstrapper>();
        }

        private void Update()
        {
            if (_playing == false) return;

            if (EnemyHeroes.Count == 0)
            {
                Win();

                _playing = false;
            }

            if (PlayerHeroes.Count == 0)
            {
                Lose();

                _playing = false;
            }
        }

        public void RenderTringle()
        {
            var hero = PlayerHeroes.FirstOrDefault(hero => hero.Skill != null);

            if (hero == null) return;

            var tringle = Instantiate(_tringle, hero.transform);//, new Vector3(0, 1.75f, 0), Quaternion.identity, hero.transform);

            tringle.transform.localPosition = new Vector3(0, 2f, 0);

            hero.OnClickDestroy.Add(tringle);
        }

        public void RenderTringle(Hero hero)
        {
            var tringle = Instantiate(_tringle, hero.transform);//, new Vector3(0, 1.75f, 0), Quaternion.identity, hero.transform);

            tringle.transform.localPosition = new Vector3(0, 1.75f, 0);

            hero.OnClickDestroy.Add(tringle);
        }

        public Hero FindNearHero(Hero currentHero)
        {
            var isPlayerHero = currentHero.IsPlayerHero;

            List<Hero> heroes = isPlayerHero ? EnemyHeroes : PlayerHeroes;
            Hero nearHero;

            if (heroes.Count == 0) return null;

            nearHero = heroes[0];

            foreach (var hero in heroes)
            {
                if(nearHero.GetDistance(hero.transform.position) > hero.GetDistance(currentHero.transform.position))
                {
                    nearHero = hero;
                }
            }

            return nearHero;
        }

        public void RemoveHero(Hero hero)
        {
            if(hero.IsPlayerHero)
            {
                _playerHeroesDied++;
            }

            var heroes = hero.IsPlayerHero ? PlayerHeroes : EnemyHeroes;

            heroes.Remove(hero);
        }

        public void EnableHeroes()
        {
            FindTargetForHeroes();

            PlayerHeroes.ForEach(hero =>
            {
                hero.HeroAttack.Heroes = this;
                hero.HeroAttack.enabled = true;

                hero.SpendEnergy();
            });

            EnemyHeroes.ForEach(hero =>
            {
                hero.HeroAttack.Heroes = this;
                hero.HeroAttack.enabled = true;
            });

            HideMenues();
            ActiveMenues();

            _playing = true;

            GameStarter = true;
        }

        public void ActiveMenues()
        {
            foreach (var menu in _activatingMenues)
            {
                menu?.SetActive(true);
            }
        }

        public void HideMenues()
        {
            foreach(var menu in _deactiveMenues)
            {
                menu?.SetActive(false);
            }
        }

        private void FindTargetForHeroes()
        {
            var index = 0;

            foreach (var hero in PlayerHeroes)
            {
                if (EnemyHeroes.Count - 1 < index) index--;

                hero.HeroAttack.SetTarget(EnemyHeroes[index].HeroAttack);

                index++;
            }

            index = 0;

            foreach (var hero in EnemyHeroes)
            {
                if (PlayerHeroes.Count - 1 < index) index--;

                hero.HeroAttack.SetTarget(PlayerHeroes[index].HeroAttack);

                index++;
            }
        }

        private void Win()
        {
            _winMenu.Show();

            foreach(var hero in _playerStats.AttackingBusiness.Security)
            {
                if (hero == null) return;


                if(_playerStats.AttackingBusiness.Bot != null)
                {
                    _playerStats.AttackingBusiness.Bot.Heroes.Add(hero.Hero);
                }
            }

            _playerStats.AttackingBusiness.Security.Clear();

            if(_playerStats.AttackingBusiness.Bot != null)
            {
                _playerStats.AttackingBusiness.Bot.Businesses.Remove(_playerStats.AttackingBusiness);
            }

            _botBootstrapper.StopAttackAllBot();
            
            _playerStats.Add(_playerStats.AttackingBusiness);

            _playerStats.AttackingBusiness.StreetName = _playerStats.LastSceneName;
        }

        private void Lose()
        {
            _loseMenu.Show();
        }
    }
}