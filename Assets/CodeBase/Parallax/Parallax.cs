using UnityEngine;

namespace CodeBase.Effects.Parallax
{
    public class Parallax : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] [Range(0f, 1f)] private float strength;

        private Vector3 _previousPosition;

        private void Start()
        {
            _previousPosition = target.position;
        }

        private void Update()
        {
            var targetPosition = target.position;

            var delta = targetPosition - _previousPosition;

            _previousPosition = targetPosition;

            transform.position += delta * strength;
        }
    }
}