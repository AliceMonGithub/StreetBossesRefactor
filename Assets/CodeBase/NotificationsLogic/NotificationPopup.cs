using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UltEvents;
using UnityEngine;

namespace CodeBase.NotificationsLogic
{
    [Serializable]
    public class NotificationInfo
    {
        public string Title;
        public string Text;

        public Business Business;

        public NotificationInfo(string title, string text, Business business)
        {
            Title = title;
            Text = text;

            Business = business;
        }
    }

    public class NotificationPopup : MonoBehaviour
    {
        [SerializeField] private UltEvent _onShowNotificationEvent;

        [Space]

        [SerializeField] private TMP_Text _notificationText;

        public List<NotificationInfo> _notifications;
        public List<NotificationInfo> _allNotification;

        private bool _showNotification;

        public List<NotificationInfo> History => TakeLast(3);

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void ShowNotification(string title, string text, Business business)
        {
            if(_showNotification)
            {
                _notifications.Add(new NotificationInfo(title, text, business));

                _allNotification.Add(new NotificationInfo(title, text, business));

                return;
            }

            _notificationText.text = title;

            _allNotification.Add(new NotificationInfo(title, text, business));

            _showNotification = true;

            _onShowNotificationEvent.Invoke();
        }

        public void TryShowNextNotification()
        {
            if(_notifications.Count != 0)
            {
                _notificationText.text = _notifications[0].Title;

                _notifications.RemoveAt(0);

                _showNotification = true;

                _onShowNotificationEvent.Invoke();
            }
            else
            {
                _showNotification = false;
            }
        }

        public List<NotificationInfo> TakeLast(int count)
        {
            var result = new List<NotificationInfo>();

            if(_allNotification.Count > 3)
            {
                int begin = _allNotification.Count - count;

                if (begin < 0)
                    begin = 0;

                for (; begin < _allNotification.Count; begin++)
                {
                    result.Add(_allNotification[begin]);
                }

                return result;
            }

            foreach (var item in _allNotification)
            {
                result.Add(item);
            }

            return result;
        }
    }
}