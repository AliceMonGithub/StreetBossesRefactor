using CodeBase.NotificationsLogic;
using System.Collections;
using UnityEngine;

namespace Assets.CodeBase.NotificationsLogic
{
    public class TestNotification : MonoBehaviour
    {
        [SerializeField] private NotificationPopup _popup;

        [SerializeField] private float _time;

        private int _count;

        void Start()
        {
            Invoke(nameof(SendNotification), _time);
        }

        private void SendNotification()
        {
            _popup.ShowNotification("Your business is under attack", "", null);

            _count++;

            Invoke(nameof(SendNotification), _time);
        }
    }
}