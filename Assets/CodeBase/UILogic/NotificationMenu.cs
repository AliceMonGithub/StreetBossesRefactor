using Assets.CodeBase.UILogic;
using CodeBase.CameraLogic;
using CodeBase.NotificationsLogic;
using SceneLogic;
using System.Collections.Generic;
using UltEvents;
using UnityEngine;
using Zenject;

namespace CodeBase.UILogic
{
    public class NotificationMenu : MonoBehaviour
    {
        [SerializeField] private UltEvent _onShow;
        [SerializeField] private UltEvent _onHide;

        [Space]        

        [SerializeField] private NotificationIcon _iconPrefab;
        [SerializeField] private Transform _parent;

        [Space]

        [SerializeField] private NotificationPopup _popup;
        [SerializeField] private CameraMovement _camera;

        private List<NotificationIcon> _icons = new List<NotificationIcon>();

        private LoadCurtain _loadCurtain;

        [Inject]
        private void Construct(LoadCurtain loadCurtain)
        {
            _loadCurtain = loadCurtain;
        } 

        private void Awake()
        {
            _popup = FindObjectOfType<NotificationPopup>();
        }

        public void Show()
        {
            _onShow.Invoke();
        }

        public void Hide()
        {
            _onHide.Invoke();
        }

        public void Render()
        {
            Clear();

            Create();
        }

        private void Create()
        {
            var reverse = _popup.History;

            reverse.Reverse();

            reverse.ForEach(item =>
            {
                var action = new NotificationAction();

                if(item.Title == "Your business is under attack")
                {
                    action = NotificationAction.GoToBusiness;
                }

                var instance = Instantiate(_iconPrefab, _parent);

                instance.Initialize(item.Title, item.Text, action, this, item.Business, _loadCurtain, _camera);

                _icons.Add(instance);
            });
        }



        private void Clear()
        {
            _icons.ForEach(icon => Destroy(icon.gameObject));

            _icons.Clear();
        }
    }
}