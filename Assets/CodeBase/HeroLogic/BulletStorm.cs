using Assets.CodeBase;
using UnityEngine;
using Zenject;

namespace HeroLogic
{
    public class BulletStorm : SkillEffect
    {
        [SerializeField] private DamageHero _prefab;

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
                var bullet = Instantiate(_prefab, _hero.Transform.position + new Vector3(0, 5, 0), Quaternion.identity, _hero.Transform);

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