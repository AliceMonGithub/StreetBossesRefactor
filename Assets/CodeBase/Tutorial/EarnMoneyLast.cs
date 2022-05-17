using Assets.CodeBase.TutorialLogic;
using System.Collections;
using UltEvents;
using UnityEngine;

namespace Assets.CodeBase.Tutorial
{
    public class EarnMoneyLast : MonoBehaviour
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
            if (_tutorialInfo.EarnMoneyQuest)
            {
                _dialog.Show(_startupText);

                _onShow.Invoke();

                _tutorialInfo.EarnMoneyQuest = false;
            }
        }
    }
}