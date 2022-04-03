using System.Collections;
using UltEvents;
using UniRx;
using UnityEngine;

namespace Assets.CodeBase.TutorialLogic
{
    public class HeroesFirstUpgradeTutorial : MonoBehaviour
    {
        [SerializeField] private UltEvent _onCloseDialog;
        [SerializeField] private UltEvent _onShow;

        [Space]

        [TextArea, SerializeField] private string _startupText;

        [Space]

        [SerializeField] private TutorialInfo _tutorialInfo;

        [SerializeField] private Dialog _dialog;

        private CompositeDisposable _disposable = new CompositeDisposable();

        private void Update()
        {
            TryShow();
        }

        public void TryShow()
        {
            if (_tutorialInfo.UpgradeHeroesFirstHelp)
            {
                _dialog.OnCloseDialog = _onCloseDialog;

                _dialog.Show(_startupText);

                _onShow.Invoke();

                _tutorialInfo.UpgradeHeroesFirstHelp = false;
            }
        }
    }
}