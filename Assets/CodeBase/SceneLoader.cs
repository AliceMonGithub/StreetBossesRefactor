using Assets.CodeBase.UILogic;
using System;
using UnityEngine.SceneManagement;

namespace CodeBase
{
    public class SceneLoader
    {
        private readonly LoadCurtain _loadCurtain;

        public SceneLoader(LoadCurtain loadCurtain)
        {
            _loadCurtain = loadCurtain;
        }

        public void LoadScene(string sceneName)
        {
            if (GetActiveScene().name == sceneName) return;

            _loadCurtain.SceneName = sceneName;

            _loadCurtain.Show();

            _loadCurtain.OnShow += Load;
        }

        public Scene GetActiveScene() =>
            SceneManager.GetActiveScene();

        private void Load(string sceneName) => 
            SceneManager.LoadScene(sceneName);
    }
}