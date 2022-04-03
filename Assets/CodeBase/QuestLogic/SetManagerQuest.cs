using System.Collections;
using UnityEngine;

namespace CodeBase.QuestLogic
{
    [CreateAssetMenu(fileName = "Quest", menuName = "Quests/SetManagerQuest")]
    public class SetManagerQuest : Quest
    {
        public bool Manager { get; set; }

        public override void Active()
        {
        }

        public override void CheckComplete()
        {
            Progress = 0;
            MaxProgress = 1;

            if (Manager)
            {
                Progress = 1;

                Complete = true;

                OnComplete.Invoke();
            }
        }

        public void SetManager()
        {
            Manager = true;
        }
    }
}