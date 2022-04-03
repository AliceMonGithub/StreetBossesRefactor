using HeroLogic;
using System.Linq;
using UniRx;
using UnityEngine;

namespace CodeBase.QuestLogic
{
    [CreateAssetMenu(fileName = "Quest", menuName = "Quests/NewHeroQuest")]
    public class NewHeroQuest : Quest
    {
        [SerializeField] private Hero _hero;

        private CompositeDisposable _disposable = new CompositeDisposable();

        public override void Active()
        {
            PlayerStats.Heroes.Subscribe(_ => CheckComplete()).AddTo(_disposable);
        }

        public override void CheckComplete()
        {
            if (PlayerStats.Heroes.Value.Any(hero => _hero == hero))
            {
                Complete = true;

                OnComplete.Invoke();
            }
        }
    }
}
