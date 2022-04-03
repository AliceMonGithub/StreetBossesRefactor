using System.Collections;
using UnityEngine;

namespace CodeBase.QuestLogic
{
    [CreateAssetMenu(fileName = "Quest", menuName = "Quests/EarnMoneyQuest")]
    public class EarnMoneyQuest : Quest
    {
        [SerializeField] private int _money;

        public override void Active()
        {
        }

        public override void CheckComplete()
        {
            Progress = PlayerStats.Money.Value;
            MaxProgress = _money;
            
            if (PlayerStats.Money.Value >= _money)
            {
                Complete = true;

                OnComplete.Invoke();
            }
        }
    }
}