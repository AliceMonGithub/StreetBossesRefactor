using CodeBase;
using CodeBase.QuestLogic;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.CodeBase
{
    public class CurrentQuestBehavior : MonoBehaviour
    {
        [SerializeField] private PlayerStats _playerStats;

        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _progress;
        [SerializeField] private GameObject _gameObject;


        void Update()
        {
            if (_playerStats.Quests.Count == 0)
            {
                _gameObject.SetActive(false);

                return;
            }

            _gameObject.SetActive(true);

            var quest = _playerStats.Quests[_playerStats.Quests.Count - 1];

            _name.text = quest.Name;
            _progress.text = quest.Progress + "/" + quest.MaxProgress;
        }
    }
}