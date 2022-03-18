using HeroLogic;
using UnityEngine;
using Zenject;

namespace Assets.CodeBase.HeroLogic
{
    public class TraceBullet : SkillEffect
    {
        [SerializeField] private Bullet _bullet;

        private SelectEnemyMenu _selectEnemyMenu;
        private HeroAttack _hero;

        private void Awake()
        {
            _selectEnemyMenu = FindObjectOfType<SelectEnemyMenuInstaller>().Menu;
        }

        public override void Active()
        {
            _selectEnemyMenu.Show();

            Time.timeScale = 0;
        }

        public void Update()
        {
            if (Input.GetButtonDown("Fire1") && _hero != null)
            {
                var bullet = Instantiate(_bullet, transform.position + Vector3.up * 0.5f, Quaternion.Euler(0, 0, -90));

                bullet.Target = _hero;

                _selectEnemyMenu.Hide();

                Time.timeScale = 1f;
            }

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            HeroAttack hero;

            if (Physics.Raycast(ray, out var hit))
            {
                hero = hit.collider.GetComponent<HeroAttack>();

                if (hero.Hero.IsPlayerHero) return;

                _hero = hero;

                _hero.Hero.SpriteRenderer.color = new Color(20, 0, 0, 1f);

                return;
            }

            if (_hero != null)
            {
                _hero.Hero.SpriteRenderer.color = Color.white;
            }

            _hero = null;
        }
    }
}