using System.Collections.Generic;
using UltEvents;
using UnityEngine;

namespace CodeBase.UILogic
{
    public class TradeMenu : MonoBehaviour
    {
        [SerializeField] private UltEvent _onInitialize;
        [SerializeField] private UltEvent _onClose;

        [SerializeField] private TradeIcon _icon;
        [SerializeField] private Transform _grid;

        [Space]

        [SerializeField] private PlayerStats _playerStats;

        private List<TradeIcon> _icons = new List<TradeIcon>();

        public void Initialize()
        {
            Render();

            _onInitialize.Invoke();
        }

        public void Close()
        {
            _onClose.Invoke();
        }

        private void Render()
        {
            Clear();

            _playerStats.TradeRequests.ForEach(request =>
            {
                var icon = Instantiate(_icon, _grid);

                icon.Initialize(request);

                _icons.Add(icon);
            });
        }

        private void Clear()
        {
            _icons.ForEach(icon =>
            {
                Destroy(icon.gameObject);
            });

            _icons.Clear();
        }
    }
}