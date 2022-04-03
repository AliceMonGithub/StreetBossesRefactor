using HeroLogic;
using UniRx;
using UnityEngine;

namespace Assets.CodeBase.SkillMenu
{
    public class SkillBehavior : MonoBehaviour
    {
        [SerializeField] private SelectEnemyMenu _selectMenu;
        [SerializeField] private GameObject _selectEnemyMenu;

        [SerializeField] private AttackHeroes _heroes;

        private CompositeDisposable disposable = new CompositeDisposable();

        private HeroAttack _hero;

        private HeroAttack _playerHero;

        private HeroAttack _oldPlayerHero;
        private HeroAttack _oldEnemyHero;

        private bool _menuEnabled;
        private bool _enabled = true;

        public bool Enabled { get => _enabled; set => _enabled = value; }

        private void Update()
        {
            if (_enabled == false) return;

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            HeroAttack hero;

            if (Physics.Raycast(ray, out var hit))
            {
                hero = hit.collider.GetComponent<HeroAttack>();

                if (hero.Hero.Skill == null && hero.Target || _heroes.GameStarter == false || _menuEnabled) return;

                if(_oldPlayerHero != null && _oldPlayerHero != hero)
                {
                    _oldPlayerHero.Hero.SpriteRenderer.color = Color.white;
                }

                _oldPlayerHero = hero;

                if (Input.GetButtonDown("Fire1") && _hero != null)
                {
                    if(_hero.Target == null)
                    {
                        _selectEnemyMenu.SetActive(true);

                        FindEnemy();

                        Time.timeScale = 0f;

                        _menuEnabled = true;
                    }
                    else
                    {
                        _selectMenu.Initialize(hero.Hero, this);

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

                                skill.Active();

                                _playerHero.Hero.Skill = null;

                                _menuEnabled = false;

                                _selectMenu.Hide();
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