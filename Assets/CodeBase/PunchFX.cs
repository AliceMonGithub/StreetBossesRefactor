using System.Collections;
using UnityEngine;

namespace Assets.CodeBase
{
    public class PunchFX : MonoBehaviour
    {
        [SerializeField] private GameObject _effect;

        public void InstantiateFX()
        {
           // var effect = GetRandomEffect();

            Instantiate(_effect, transform.position, Quaternion.identity);
        }
    }
}