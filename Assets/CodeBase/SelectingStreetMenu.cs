using Assets.CodeBase;
using CodeBase;
using SceneLogic;
using UltEvents;
using UnityEngine;
using Zenject;

public class SelectingStreetMenu : MonoBehaviour
{
    private LoadCurtain _loadCurtain;

    [Inject]
    private void Construct(LoadCurtain loadCurtain)
    {
        _loadCurtain = loadCurtain;
    }

    public void LoadStreet(string sceneName)
    {
        if(_loadCurtain.CurrentSceneName != sceneName)
        {
            _loadCurtain.LoadScene(sceneName);
        }
    }    //private SceneLoader _sceneLoader;
    //[SerializeField] private UltEvent _onShow;
    //[SerializeField] private UltEvent _onHide;

    //[Inject]
    //private void Construct(SceneLoader sceneLoader)
    //{
    //    _sceneLoader = sceneLoader;
    //}

    //public void SelectStreet(string sceneName)
    //{
    //    _sceneLoader.LoadScene(sceneName);
    //}

    //public void Show() => _onShow.Invoke();
    
    //public void Hide() => _onHide.Invoke();
}
