﻿using Assets.CodeBase.TutorialLogic;
using CodeBase;
using CodeBase.CameraLogic;
using CodeBase.QuestLogic;
using System.Collections;
using UltEvents;
using UniRx;
using UnityEngine;

namespace Assets.CodeBase.Tutorial
{
    public class BusinessTakenTutorial : MonoBehaviour
    {
        [SerializeField] private UltEvent _onCloseDialog;

        [SerializeField] private UltEvent _onShow;

        [Space]

        [TextArea, SerializeField] private string _startupText;

        [SerializeField] private Quest _earningQuest;

        [Space]

        [SerializeField] private PlayerStats _playerStats;
        [SerializeField] private TutorialInfo _tutorialInfo;
        [SerializeField] private CameraMovement _camera;

        [SerializeField] private Dialog _dialog;

        private CompositeDisposable _disposable = new CompositeDisposable();

        private void Update()
        {
            TryShow();
        }

        public void TryShow()
        {
            if (_tutorialInfo.BusinessTakenHelp)
            {
                _dialog.OnCloseDialog = _onCloseDialog;

                _dialog.Show(_startupText);

                _onShow.Invoke();

                _tutorialInfo.BusinessTakenHelp = false;
            }
        }

        public void MoveRandomBusiness()
        {
            if (_playerStats.TryFindQuest(_earningQuest)) return;

            var business = _playerStats.Businesses.Value[0];

            business.UpgradeIcon.ShowTringle();
            //business.BusinessImage.OnLight.Invoke();
            _camera.MoveToTarget(business.BusinessImage.transform);
        }
    }
}