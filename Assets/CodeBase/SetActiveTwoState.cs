using UnityEngine;

namespace CodeBase
{
    public class SetActiveTwoState : MonoBehaviour
    {
        private bool _isActive;
        
        public void SetActive()
        {
            if (_isActive)
            {
                gameObject.SetActive(false);

                _isActive = false;

                return;
            }

            gameObject.SetActive(true);

            _isActive = true;
        }
    }
}