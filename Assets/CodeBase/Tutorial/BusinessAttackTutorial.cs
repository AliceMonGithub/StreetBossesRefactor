using Assets.CodeBase.TutorialLogic;
using CodeBase;
using CodeBase.QuestLogic;
using System.Collections;
using UltEvents;
using UniRx;
using UnityEngine;

namespace Assets.CodeBase
{
    public class BusinessAttackTutorial : MonoBehaviour
    {
        [SerializeField] private UltEvent _onCloseDialog;

        [TextArea, SerializeField] private string _dialogText;

        [Space]

        [SerializeField] private Quest _attackBusinessQuest;

        [Space]

        [SerializeField] private PlayerStats _playerStats;
        [SerializeField] private TutorialInfo _tutorialInfo;

        [Space]

        [SerializeField] private Dialog _dialog;

        private CompositeDisposable _disposable;

        private void Update()
        {
            TryShow();
        }

        private void TryShow()
        {
            _dialog.OnCloseDialog = _onCloseDialog;

            _dialog.Show(_dialogText);

            _playerStats.Quests.Add(_attackBusinessQuest);
        }
    }
}