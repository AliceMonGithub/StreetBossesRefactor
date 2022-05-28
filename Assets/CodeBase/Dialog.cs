using System.Collections;
using UltEvents;
using UniRx;
using UnityEngine;

namespace Assets.CodeBase.TutorialLogic
{
    public class Dialog : MonoBehaviour
    {
        [SerializeField] private UltEvent _onShow;
        [SerializeField] private UltEvent _onEndDialog;

        [SerializeField] private UltEvent _onFinishAnimation;

        [SerializeField] private GameObject[] _hidingObjects;

        [SerializeField] private PrintingTextBehavior _text;

        [SerializeField] private GameObject _dialog;

        private CompositeDisposable _disposable = new CompositeDisposable();

        public UltEvent OnCloseDialog { get; set; }

        public void Show(string text)
        {
            if(_hidingObjects.Length == 0)
            {
                _dialog.SetActive(true);

                _text?.PrintText(text);

                _onShow.Invoke();

                _disposable.Clear();

                return;
            }

            Observable.EveryUpdate().Subscribe(_ =>
            {
                var canShow = true;

                foreach (var gObject in _hidingObjects)
                {
                    if (gObject == null) continue;

                    if (gObject.activeSelf)
                    {
                        canShow = false;
                    }
                }

                if(canShow)
                {
                    _dialog.SetActive(true);

                    _text?.PrintText(text);

                    _onShow.Invoke();

                    _disposable.Clear();
                }

            }).AddTo(_disposable);
        }

        public void OnFinishAnimation()
        {
            _onFinishAnimation.Invoke();
        }

        public void OnEndDialogEvent()
        {
            _onEndDialog.Invoke();
        }

        public void StopTime()
        {
            Time.timeScale = 0f;
        }

        public void ResumeTime()
        {
            Time.timeScale = 1f;
        }

        public void CloseDialogEvent()
        {
            if(OnCloseDialog != null)
            {
                OnCloseDialog.Invoke();
            }
        }
    }
}