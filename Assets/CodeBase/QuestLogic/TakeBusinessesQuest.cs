using CodeBase.QuestLogic;
using System.Collections;
using UnityEngine;

namespace Assets.CodeBase.QuestLogic
{
    [CreateAssetMenu(fileName = "Quest", menuName = "Quests/TakeBusinessesQuest")]
    public class TakeBusinessesQuest : Quest
    {
        [SerializeField] private string _streetName;
        [SerializeField] private int _businessCount;

        public override void Active()
        {
        }

        public override void CheckComplete()
        {
            var businesses = PlayerStats.Businesses.Value.FindAll(business => business.StreetName == _streetName);

            MaxProgress = _businessCount;
            Progress = businesses.Count;

            if (businesses.Count >= _businessCount)
            {
                Complete = true;

                OnComplete.Invoke();
            }
        }
    }
}