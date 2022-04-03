using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.QuestLogic
{
    [CreateAssetMenu(fileName = "Quest", menuName = "Quests/GoStreetQuest")]
    public class GoStreetQuest : Quest
    {
        [SerializeField] private string _sceneName;

        public override void Active()
        {
        }

        public override void CheckComplete()
        {
            Progress = 0;
            MaxProgress = 1;

            if(_sceneName == SceneManager.GetActiveScene().name)
            {
                Complete = true;

                OnComplete.Invoke();
            }
        }
    }
}