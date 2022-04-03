using SceneLogic;
using System;
using System.Collections;
using UltEvents;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneLogic
{
    public class LoadCurtain : MonoBehaviour
    {
        [SerializeField] private UltEvent _onLoadingStart;
        [SerializeField] private UltEvent _onLoadingFinish;

        public readonly SceneLoader SceneLoader = new SceneLoader();

        private string _sceneName;

        public Action OnHideEvent;

        public string CurrentSceneName => SceneLoader.CurrentSceneName;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void LoadScene(string sceneName)
        {
            _sceneName = sceneName;

            _onLoadingStart.Invoke();
        }

        public void Load()
        {
            SceneLoader.LoadScene(_sceneName, _onLoadingFinish.Invoke);
        }

        public void OnHide()
        {
            OnHideEvent?.Invoke();
        }
    }
}