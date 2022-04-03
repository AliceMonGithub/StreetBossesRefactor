using Assets.CodeBase.TutorialLogic;
using System.Collections;
using UltEvents;
using UnityEngine;

namespace Assets.CodeBase.Tutorial
{
    public class SetManagerTutorial : MonoBehaviour
    {
        [SerializeField] private UltEvent _onShow;

        [Space]

        [TextArea, SerializeField] private string _startupText;

        [Space]

        [SerializeField] private TutorialInfo _tutorialInfo;

        [SerializeField] private Dialog _dialog;

        private void Update()
        {
            TryShow();
        }

        public void TryShow()
        {
            if (_tutorialInfo.SetManagerHelp)
            {
                _dialog.Show(_startupText);

                _onShow.Invoke();

                _tutorialInfo.SetManagerHelp = false;
            }
        }
    }
}