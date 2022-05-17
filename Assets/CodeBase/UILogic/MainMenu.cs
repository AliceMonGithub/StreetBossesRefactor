using SceneLogic;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.CodeBase.UILogic
{
    public class MainMenu : MonoBehaviour
    {
        private LoadCurtain _loadCurtain;

        [Inject]
        private void Construct(LoadCurtain loadCurtain)
        {
            _loadCurtain = loadCurtain;
        }

        public void LoadScene(string sceneName)
        {
            _loadCurtain.LoadScene(sceneName);
        }
    }
}