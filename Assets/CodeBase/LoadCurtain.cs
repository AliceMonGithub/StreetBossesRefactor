using SceneLogic;
using System.Collections;
using UltEvents;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneLogic
{
    public class LoadCurtain : MonoBehaviour
    {
        [SerializeField] private UltEvent _onLoadingStart;
        [SerializeField] private UltEvent _onLoadingFinish;

        private string _sceneName;

        public readonly SceneLoader SceneLoader = new SceneLoader();

        public string CurrentSceneName => SceneLoader.CurrentSceneName;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);

            //_sceneName = sceneName;

            //_onLoadingStart.Invoke();
        }

        public void Load()
        {
            SceneLoader.LoadScene(_sceneName, _onLoadingFinish.Invoke);
        }
    }
}