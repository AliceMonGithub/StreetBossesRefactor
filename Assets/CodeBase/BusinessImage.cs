using UltEvents;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.CodeBase
{
    public class BusinessImage : MonoBehaviour
    {
        [SerializeField] private UltEvent _onUpgrade;
        [SerializeField] private UltEvent _onLight;

        [SerializeField] private Transform _upperPoint;

        [Space]

        [SerializeField] private GameObject _tringle;

        public UltEvent OnUpgrade => _onUpgrade;
        public UltEvent OnLight => _onLight;

        public Transform UpperPoint => _upperPoint;

        public void InstantiateTringle()
        {
            Instantiate(_tringle, _upperPoint);
        }
    }
}