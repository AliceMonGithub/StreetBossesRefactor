using System;
using UltEvents;
using UnityEngine;

namespace CodeBase.Mao_AddiotionUiScript
{
    public class TriggerButton : MonoBehaviour
    {
        public UltEvent OnTrue;
        public UltEvent OnFalse;

        [SerializeField] private bool _value;

        private void Start() => InvokeUlt();

        [ContextMenu("Change")]
        public void Change()
        {
            _value = !_value;
            InvokeUlt();
        }

        private void InvokeUlt()
        {
            if(_value) OnTrue.Invoke();
            else OnFalse.Invoke();
        }
    }
}