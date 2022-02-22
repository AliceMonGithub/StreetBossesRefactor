using UltEvents;
using UnityEngine;

namespace CodeBase.Mao_AddiotionUiScript
{
    public abstract class ProviderLeanElementT<T> : MonoBehaviour
    {
        public T Value;
        public UltEvent<T> OnProvidee;
        
        private void Start()=>OnProvidee.Invoke(Value);
    }
}