using CodeBase;
using CodeBase.QuestLogic;
using System.Collections;
using System.Collections.Generic;
using UltEvents;
using UnityEngine;

namespace Assets.CodeBase
{
    public class QuestMenu : MonoBehaviour
    {
        [SerializeField] private UltEvent _onEnable;

        [SerializeField] private PlayerStats _playerStats;

        [SerializeField] private Transform _grid;
        [SerializeField] private QuestIcon _icon;
        [SerializeField] private QuestIcon _completedQuestIcon;

        private List<GameObject> _icons = new List<GameObject>();

        private void OnEnable()
        {
            Render();

            _onEnable.Invoke();
        }

        private void OnDisable()
        {
            _icons.ForEach(icon => Destroy(icon));

            _icons.Clear();
        }

        private void Render()
        {
            List<Quest> runnedQuests = new List<Quest>();
            List<Quest> completedQuests = new List<Quest>();

            foreach(var quest in _playerStats.Quests)
            {
                if(quest.Complete)
                {                    
                    completedQuests.Add(quest);
                }
                else
                {
                    runnedQuests.Add(quest);
                }
            }

            foreach(var quest in runnedQuests)
            {
                var icon = Instantiate(_icon, _grid);

                icon.Initialize(quest);

                _icons.Add(icon.gameObject);
            }

            foreach(var quest in completedQuests)
            {
                var icon = Instantiate(_completedQuestIcon, _grid);

                icon.Initialize(quest);

                _icons.Add(icon.gameObject);
            }
        }
    }
}