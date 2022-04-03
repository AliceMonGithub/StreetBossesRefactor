using System.Collections;
using UltEvents;
using UniRx;
using UnityEngine;

namespace Assets.CodeBase.TutorialLogic
{
    public class Dialog : MonoBehaviour
    {
        [SerializeField] private UltEvent _onShow;

        [SerializeField] private GameObject[] _hidingObjects;

        [SerializeField] private PrintingTextBehavior _text;

        [SerializeField] private GameObject _dialog;

        private CompositeDisposable _disposable = new CompositeDisposable();

        public UltEvent OnCloseDialog { get; set; }

        public void Show(string text)
        {
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

                    _text.PrintText(text);

                    _onShow.Invoke();

                    _disposable.Clear();
                }
            }).AddTo(_disposable);
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