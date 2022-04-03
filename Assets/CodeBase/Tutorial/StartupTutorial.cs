using Assets.CodeBase.TutorialLogic;
using CodeBase;
using System.Collections;
using UltEvents;
using UnityEngine;

namespace Assets.CodeBase
{
    public class StartupTutorial : MonoBehaviour
    {
        [SerializeField] private UltEvent _onCloseDialog;
        [SerializeField] private UltEvent _onShow;

        [Space]

        [TextArea, SerializeField] private string _startupText;

        [Space]

        [SerializeField] private TutorialInfo _tutorialInfo;

        [SerializeField] private Dialog _dialog;

        public void TryShow()
        {
            if(_tutorialInfo.StartupHelp)
            {
                _dialog.OnCloseDialog = _onCloseDialog;

                _dialog.Show(_startupText);

                _onShow.Invoke();

                _tutorialInfo.StartupHelp = false;
            }
        }
    }
}