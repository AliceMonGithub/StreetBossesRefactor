using HeroLogic;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Assets.CodeBase
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private HeroAttack _heroAttack;

        [Space]

        [SerializeField] private Image _slider;
        [SerializeField] private TMP_Text _healthText;

        [Space]

        [SerializeField] private GameObject _root;

        private CompositeDisposable _disposable = new CompositeDisposable();

        private void Start()
        {
            _heroAttack.HealthEvent.Subscribe(action => Render()).AddTo(_disposable);
        }

        private void OnDisable()
        {
            _disposable.Clear();
        }

        private void Render()
        {
            if (_heroAttack.Health <= 0)
            {
                Destroy(_root);

                return;
            }

            _slider.fillAmount = Mathf.Clamp(_heroAttack.Health / (_heroAttack.StartHealth / 100f) / 100f, 0f, 1f);

            _healthText.text = Mathf.Clamp(_heroAttack.Health, 0, Mathf.Infinity).ToString();
        }
    }
}