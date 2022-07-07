using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.BotLogic
{
    public class BotBootstrapper : MonoBehaviour
    {
        [SerializeField] private List<Bot> _bots;

        [Space]

        [SerializeField] private PlayerStats _playerStats;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);

            StartCoroutine(TryAttack());
        }

        public void StopAttackAllBot()
        {
            _bots.ForEach(bot => bot.InvokeOnLose());
        }

        private IEnumerator TryAttack()
        {
            while(true)
            {
                _playerStats.SetNeutralBusinesses();

                foreach(var bot in _bots)
                {
                    bot.TryAttack();
                }

                yield return new WaitForSeconds(3f);
            }
        }
    }
}