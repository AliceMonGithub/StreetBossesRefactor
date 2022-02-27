//using Assets.CodeBase.UILogic;
using System;
using UniRx;
using UnityEngine.SceneManagement;

namespace SceneLogic
{
    public class SceneLoader
    {
        private CompositeDisposable _loadingDisposable = new CompositeDisposable();

        public float Progress { get; private set; }

        public string CurrentSceneName => SceneManager.GetActiveScene().name;

        public void LoadScene(string sceneName, Action onComplete = null)
        {
            SceneManager.LoadSceneAsync(sceneName)
                .AsAsyncOperationObservable()
                .Do(loading =>
                {
                    Progress = loading.progress;

                }).Subscribe(onFinish =>
                {
                    onComplete?.Invoke();

                }).AddTo(_loadingDisposable);
        }
    }
}