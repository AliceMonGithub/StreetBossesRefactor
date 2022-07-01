using HeroLogic;
using System.Collections;
using UnityEngine;

namespace Assets.CodeBase
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private int _damage;

        [SerializeField] private float _speed;

        [SerializeField] private Transform _transform;

        private Vector3 _velosity;

        public HeroAttack Target { get; set; }

        private void Update()
        {
            //_transform.eulerAngles = Vector3.zero;
            
            _transform.position = Vector3.SmoothDamp(_transform.position, Target.Transform.position + Vector3.up * 1f, ref _velosity, 0.1f, _speed);

            if (Vector3.Distance(_transform.position, Target.Transform.position + Vector3.up * 1f) < 0.1)
            {
                Target.DecreaseHealth(_damage);

                Destroy(gameObject);
            }
        }
    }
}