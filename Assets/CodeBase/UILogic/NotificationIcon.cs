using CodeBase.CameraLogic;
using CodeBase.UILogic;
using SceneLogic;
using System;
using TMPro;
using UltEvents;
using UnityEngine;

namespace Assets.CodeBase.UILogic
{
    public enum NotificationAction
    {
        Nope,
        GoToBusiness
    }

    public class NotificationIcon : MonoBehaviour
    {
        [SerializeField] private TMP_Text _notificationTitle;
        [SerializeField] private TMP_Text _notificationText;

        private NotificationAction _action;

        private NotificationMenu _menu;
        private CameraMovement _camera;
        private LoadCurtain _loadCurtain;
        private Business _business;

        public void Click()
        {
            if(_action == NotificationAction.GoToBusiness)
            {
                MoveToBusiness();
            }
        }

        public void Initialize(string title, string text, NotificationAction action, NotificationMenu menu, Business business, LoadCurtain loadCurtain, CameraMovement camera)
        {
            _notificationTitle.text = title;
            _notificationText.text = text;

            _action = action;

            _menu = menu;

            _business = business;

            _loadCurtain = loadCurtain;

            _camera = camera;
        }

        private void MoveToBusiness()
        {
            if (_business.StreetName == _loadCurtain.CurrentSceneName)
            {
                var tringles = FindObjectsOfType<Tringle>();

                foreach (var tringle in tringles)
                {
                    Destroy(tringle.gameObject);
                }

                _business.BusinessImage.OnLight.Invoke();
                _camera.MoveToTarget(_business.BusinessImage.transform);

                _menu.Hide();
            }
            else
            {
                _loadCurtain.LoadScene(_business.StreetName);
            }
        }
    }
}