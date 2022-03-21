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

        private bool _enabled = true;

        private void Update()
        {
            if (_enabled == false) return;

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            HeroAttack hero;

            if (Physics.Raycast(ray, out var hit))
            {
                hero = hit.collider.GetComponent<HeroAttack>();

                if (hero.Hero.Skill == null) return;

                if (Input.GetButtonDown("Fire1") && _hero != null)
                {
                    if(_hero.Target == null)
                    {
                        _selectEnemyMenu.SetActive(true);

                        FindEnemy();

                        Time.timeScale = 0f;
                    }
                    else
                    {
                        _selectMenu.Initialize(hero.Hero, this);

                        Time.timeScale = 0f;
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

                        if (Input.GetButtonDown("Fire1") && enemyHero != null)
                        {
                            if(skill != null)
                            {
                                skill.Target = enemyHero;

                                skill.Active();

                                _selectMenu.Hide();
                            }
                            else
                            {
                                _hero.SetTarget(enemyHero);

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