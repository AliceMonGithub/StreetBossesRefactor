using CodeBase.BotLogic;
using System.Collections;
using TMPro;
using UltEvents;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UILogic
{
    public class FillSimulation : MonoBehaviour
    {
        [SerializeField] private UltEvent _onResetEvent;

        [Space]

        [SerializeField] private ParticleSystem _particles;

        [Space]

        [SerializeField] private Image _coinImage;

        public Business Business;
        public Bot Bot;

        private float _deltaTime;

        private bool _canReset;

        private void Update()
        {
            if (_deltaTime >= Business.EarningDurication && _canReset == false)
            {
                Invoke(nameof(ResetEarning), Random.Range(0.5f, 3f));

                _canReset = true;
            }

            _deltaTime += Time.deltaTime;

            _coinImage.fillAmount = Mathf.Clamp(_deltaTime / (Business.EarningDurication / 100f) / 100f, 0f, 1f);
        }

        public void Initialize(Business business, Bot bot)
        {
            Business = business;
            Bot = bot;
        }

        public void InstantiateParticles()
        {
            Instantiate(_particles, transform.position, Quaternion.identity);
        }

        private void ResetEarning()
        {
            _deltaTime = 0f;

            _onResetEvent.Invoke();

            Bot.Money += Business.Earning;

            _canReset = false;

            CancelInvoke(nameof(ResetEarning));
        }
    }
}