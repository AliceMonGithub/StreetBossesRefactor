using CodeBase.QuestLogic;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Assets.CodeBase.QuestLogic
{
    [CreateAssetMenu(fileName = "Quest", menuName = "Quests/BusinessCountQuest")]
    public class BusinessCountQuest : Quest
    {
        [SerializeField] private int _businessCount;

        public override void Active()
        {
        }

        public override void CheckComplete()
        {
            MaxProgress = _businessCount;
            Progress = PlayerStats.Businesses.Value.Count;

            if (PlayerStats.Businesses.Value.Count >= _businessCount)
            {
                Complete = true;

                OnComplete.Invoke();
            }
        }
    }
}