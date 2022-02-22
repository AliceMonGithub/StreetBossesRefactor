using System;
using System.Collections;
using UltEvents;
using UnityEngine;

namespace Assets.CodeBase.UILogic
{
    public class LoadCurtain : MonoBehaviour
    {
        private static readonly int ShowHash = Animator.StringToHash("Show");


        public Action<string> OnShow;

        public UltEvent OnShowing;
        public UltEvent OnHideing;

        public string SceneName { get; set; }

        private void Awake()
        {
            gameObject.SetActive(true);
            Hide();
        }

        public void Show() => OnShowing.Invoke();

        public void Hide() => OnHideing.Invoke();

        public void InvokeOnShow() => OnShow?.Invoke(SceneName);
    }
}