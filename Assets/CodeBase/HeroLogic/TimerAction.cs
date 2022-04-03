using UltEvents;
using UniRx;
using UnityEngine;

namespace HeroLogic
{
    public class TimerAction : MonoBehaviour
    {
        [SerializeField] private UltEvent _action;
        [SerializeField] private UltEvent _onStart;
        [SerializeField] private UltEvent _onEnd;

        [SerializeField] private float _time;
        [SerializeField] private float _frequency;

        private CompositeDisposable _disposable = new CompositeDisposable();

        private void Awake()
        {
            _onStart.Invoke();
        }

        public void Start()
        {
            var freqTime = 0f;
            var deltaTime = 0f;

            Observable.EveryUpdate().Subscribe(action =>
            {
                freqTime += Time.deltaTime;
                deltaTime += Time.deltaTime;

                if (freqTime >= _frequency)
                {
                    _action.Invoke();

                    freqTime = 0;
                }

                if (_time <= deltaTime)
                {
                    _onEnd.Invoke();

                    _disposable.Clear();
                }

            }).AddTo(_disposable);
        }
    }
}