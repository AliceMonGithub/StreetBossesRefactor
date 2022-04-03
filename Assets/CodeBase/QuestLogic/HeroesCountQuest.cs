using UnityEngine;

namespace CodeBase.QuestLogic
{
    [CreateAssetMenu(fileName = "Quest", menuName = "Quests/HeroesCountQuest")]
    public class HeroesCountQuest : Quest
    {
        [SerializeField] private int _heroesCount;

        public override void Active()
        {
        }

        public override void CheckComplete()
        {
            MaxProgress = _heroesCount;
            Progress = PlayerStats.Heroes.Value.Count;

            if (PlayerStats.Heroes.Value.Count >= _heroesCount)
            {
                Complete = true;

                OnComplete.Invoke();
            }
        }
    }
}
