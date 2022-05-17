using CodeBase.QuestLogic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.CodeBase.QuestLogic
{
    [CreateAssetMenu(fileName = "Quest", menuName = "Quests/LoseBusinessQuest")]
    public class LoseBusinessQuest : Quest
    {
        [SerializeField] private string _streetName;

        public bool AttackLose;

        public override void Active()
        {
        }

        public override void CheckComplete()
        {
            if (AttackLose)
            {
                Complete = true;

                OnComplete.Invoke();
            }
        }

        public void Lose()
        {
            if(_streetName == PlayerStats.LastSceneName)
            {
                AttackLose = true;
            }
        }
    }
}