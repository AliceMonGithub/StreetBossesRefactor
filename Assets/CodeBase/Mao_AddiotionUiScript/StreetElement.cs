using SceneLogic;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.Mao_AddiotionUiScript
{
    public class StreetElement : MonoBehaviour
    {

        private LoadCurtain _loadCurtain;

        [Inject]
        private void Construct(LoadCurtain loadCurtain)
        {
            _loadCurtain = loadCurtain;
        }

        public void TryLoad(string sceneName)
        {
            if (sceneName == _loadCurtain.CurrentSceneName) return;

            _loadCurtain.LoadScene(sceneName);
        }
    }
}