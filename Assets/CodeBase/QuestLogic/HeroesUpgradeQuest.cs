using CodeBase.QuestLogic;
using System.Collections;
using UnityEngine;

namespace Assets.CodeBase.QuestLogic
{
    [CreateAssetMenu(fileName = "Quest", menuName = "Quests/HeroesUpgradeQuest")]
    public class HeroesUpgradeQuest : Quest
    {
        [SerializeField] private int _level;

        public override void Active()
        {

        }

        public override void CheckComplete()
        {
            var heroes = PlayerStats.Heroes.Value.FindAll(hero => hero.Level == _level);

            MaxProgress = PlayerStats.Heroes.Value.Count;
            Progress = heroes.Count;

            if(heroes.Count == PlayerStats.Heroes.Value.Count)
            {
                Complete = true;

                OnComplete.Invoke();
            }
        }
    }
}