using Assets.CodeBase.UILogic;
using CodeBase.UILogic;
using HeroLogic;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

namespace Assets.CodeBase.SkillMenu
{
    public class SkillBehavior : MonoBehaviour
    {
        [SerializeField] private SelectEnemyEffect _selectEffect;
        [SerializeField] private SkillIcons _skillIcons;

        public Hero SelectedHero;

        public bool Enabled = true;
        private bool _seekEnemy;

        private CompositeDisposable _disposable = new CompositeDisposable();

        public SkillIcons SkillIcons => _skillIcons;

        public bool SeekEnemy => _seekEnemy;

        private void OnDisable()
        {
            _disposable.Clear();
        }

        public void ActiveSkill(SkillEffect skill, Hero sender)
        {
            sender.SpriteRenderer.color = new Color(0.3f, 1, 0.3f, 1f);

            _seekEnemy = true;

            _selectEffect.Show();

            Observable.EveryUpdate().Subscribe(action =>
            {
                if (SelectedHero != null)
                {
                    if (SelectedHero.IsPlayerHero == false)
                    {
                        if (Input.GetButtonDown("Fire1"))
                        {
                            print("button");

                            if (skill != null)
                            {
                                print("skill");

                                skill.Target = SelectedHero.HeroAttack;

                                SelectedHero.OnClickDestroy.ForEach(gayObject => Destroy(gayObject));
                                SelectedHero.OnClickDestroy.Clear();

                                sender.SpriteRenderer.color = Color.white;

                                skill.Active();

                                sender.Skill = null;

                                _seekEnemy = false;

                                _selectEffect.Hide();

                                _disposable.Clear();
                            }
                        }
                    }
                }

            }).AddTo(_disposable);
        }
    }
}