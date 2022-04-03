using CodeBase;
using System.Collections;
using UnityEngine;

namespace Assets.CodeBase
{
    public class QuestsBootstrapper : MonoBehaviour
    {
        [SerializeField] private PlayerStats _playerStats;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            for (int i = 0; i < _playerStats.Quests.Count; i++)
            {
                if (_playerStats.Quests[i].Complete) continue;

                _playerStats.Quests[i].CheckComplete();
            }
        }
    }
}