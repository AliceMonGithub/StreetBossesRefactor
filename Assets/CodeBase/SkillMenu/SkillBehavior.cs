using HeroLogic;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

namespace Assets.CodeBase.SkillMenu
{
    public class SkillBehavior : MonoBehaviour
    {
        [SerializeField] private SelectEnemyMenu _selectMenu;
        [SerializeField] private GameObject _selectEnemyMenu;
        [SerializeField] private GameObject _tringle;
        
        [SerializeField] private List<GameObject> _hidingObjects;

        [SerializeField] private AttackHeroes _heroes;

        [SerializeField] private TutorialInfo _tutorialInfo;

        private CompositeDisposable disposable = new CompositeDisposable();

        private HeroAttack _hero;

        private HeroAttack _playerHero;

        private HeroAttack _oldPlayerHero;
        private HeroAttack _oldEnemyHero;

        private GameObject _enemyTringle;

        private bool _menuEnabled;
        private bool _enabled = true;

        public bool Enabled { get => _enabled; set => _enabled = value; }

        private void Update()
        {
            if (_enabled == false) return;

            //if (_hidingObjects.Any(gObject => gObject.activeSelf == true)) return;

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            HeroAttack hero;

            if (Physics.Raycast(ray, out var hit))
            {
                hero = hit.collider.GetComponent<HeroAttack>();

                if (_heroes.GameStarter == false || _menuEnabled) return;//hero.Hero.Skill == null && hero.Target || _heroes.GameStarter == false || _menuEnabled) return;

                if (_oldPlayerHero != null && _oldPlayerHero != hero)
                {
                    _oldPlayerHero.Hero.SpriteRenderer.color = Color.white;
                }

                _oldPlayerHero = hero;

                if (Input.GetButtonDown("Fire1") && _hero != null)
                {
                    if(_hero.Hero.Skill == null && _hero.MeelCombat == false)
                    {
                        _selectEnemyMenu.SetActive(true);

                        _hero.Hero.OnClickDestroy.ForEach(gObject => Destroy(gObject));
                        _hero.Hero.OnClickDestroy.Clear();

                        FindEnemy();

                        Time.timeScale = 0f;

                        _menuEnabled = true;
                    }
                    else if(_hero.Hero.Skill != null)
                    {
                        _selectMenu.Initialize(hero.Hero, this);

                        _hero.Hero.OnClickDestroy.ForEach(gObject => Destroy(gObject));
                        _hero.Hero.OnClickDestroy.Clear();

                        _playerHero = _hero;

                        Time.timeScale = 0f;

                        _menuEnabled = true;
                    }
                }

                if(_hero != null)
                {
                    if(_hero.Hero.IsPlayerHero)
                    {
                        _hero.Hero.SpriteRenderer.color = new Color(0, 0.4f, 0, 1);
                    }
                }

                if (hero.Hero.IsPlayerHero)
                {
                    _hero = hero;
                }
                else
                {
                    if (_hero != null)
                    {
                        _hero.Hero.SpriteRenderer.color = Color.white;

                        _hero = null;
                    }
                }
            }
            else
            {
                if(_hero != null)
                {
                    _hero.Hero.SpriteRenderer.color = Color.white;

                    _hero = null;
                }
            }
        }

        public void FindEnemy(SkillEffect skill = null)
        {
            _enabled = false;

            HeroAttack enemyHero = new HeroAttack();

            if(_tutorialInfo.TringleEnemyHelp)
            {
                var nearEnemy = _heroes.FindNearHero(_playerHero.Hero);

                var tringle = Instantiate(_tringle, nearEnemy.transform);

                tringle.transform.localPosition = new Vector3(0, 2f, 0);

                _enemyTringle = tringle;

                _tutorialInfo.TringleEnemyHelp = false;
            }

            Observable.EveryUpdate().Subscribe(action =>
            {
                if (_enabled == false)
                {
                    var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                    HeroAttack hero;

                    if (Physics.Raycast(ray, out var hit))
                    {
                        hero = hit.collider.GetComponent<HeroAttack>();

                        if (_oldEnemyHero != null && _oldEnemyHero != hero)
                        {
                            _oldEnemyHero.Hero.SpriteRenderer.color = Color.white;
                        }

                        _oldEnemyHero = hero;

                        if (Input.GetButtonDown("Fire1") && enemyHero != null)
                        {
                            if(skill != null)
                            {
                                skill.Target = enemyHero;

                                enemyHero.Hero.OnClickDestroy.ForEach(gObject => Destroy(gObject));
                                enemyHero.Hero.OnClickDestroy.Clear();
                                
                                skill.Active();

                                _playerHero.Hero.Skill = null;

                                _menuEnabled = false;

                                _selectMenu.Hide();

                                if (_enemyTringle != null)
                                {
                                    Destroy(_enemyTringle);
                                }
                            }
                            else
                            {
                                _hero.SetTarget(enemyHero);

                                _menuEnabled = false;

                                _selectEnemyMenu.SetActive(false);
                            }

                            _heroes.PlayerHeroes.ForEach(hero => hero.SpriteRenderer.color = Color.white);

                            Time.timeScale = 1f;

                            _enabled = true;

                            disposable.Clear();
                        }

                        if (enemyHero != null)
                        {
                            enemyHero.Hero.SpriteRenderer.color = new Color(0.4f, 0, 0, 1);
                        }

                        if (hero.Hero.IsPlayerHero == false)
                        {
                            enemyHero = hero;
                        }
                        else
                        {
                            if (enemyHero != null)
                            {
                                enemyHero.Hero.SpriteRenderer.color = Color.white;

                                enemyHero = null;
                            }
                        }
                    }
                    else
                    {
                        if (enemyHero != null)
                        {
                            enemyHero.Hero.SpriteRenderer.color = Color.white;

                            enemyHero = null;
                        }
                    }
                }

            }).AddTo(disposable);
        }
    }
}