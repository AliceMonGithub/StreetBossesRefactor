using CodeBase;
using UltEvents;
using UnityEngine;
using Zenject;

public class SelectingStreetMenu : MonoBehaviour
{
    private SceneLoader _sceneLoader;
    [SerializeField] private UltEvent _onShow;
    [SerializeField] private UltEvent _onHide;

    [Inject]
    private void Construct(SceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
    }

    public void SelectStreet(string sceneName)
    {
        _sceneLoader.LoadScene(sceneName);
    }

    public void Show() => _onShow.Invoke();
    
    public void Hide() => _onHide.Invoke();
}
