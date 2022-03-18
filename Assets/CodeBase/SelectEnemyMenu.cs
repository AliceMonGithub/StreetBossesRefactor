using UltEvents;
using UnityEngine;

namespace Assets.CodeBase
{
    public class SelectEnemyMenu : MonoBehaviour
    {
        [SerializeField] private UltEvent _showEvent;
        [SerializeField] private UltEvent _hideEvent;

        public void Show()
        {
            _showEvent.Invoke();
        }

        public void Hide()
        {
            _hideEvent.Invoke();
        }
    }
}