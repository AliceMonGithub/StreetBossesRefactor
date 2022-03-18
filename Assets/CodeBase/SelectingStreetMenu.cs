using Assets.CodeBase;
using CodeBase;
using SceneLogic;
using UltEvents;
using UnityEngine;
using Zenject;

public class SelectingStreetMenu : MonoBehaviour
{
    [SerializeField] private UltEvent _show;
    [SerializeField] private UltEvent _hide;

    private LoadCurtain _loadCurtain;

    [Inject]
    private void Construct(LoadCurtain curtain)
    {
        _loadCurtain = curtain;
    }

    public void LoadStreet(string sceneName)
    {
        if(_loadCurtain.CurrentSceneName != sceneName)
        {
            _loadCurtain.LoadScene(sceneName);
        }
    }

    public void Show()
    {
        _show.Invoke();
    }

    public void Hide()
    {
        _hide.Invoke();
    }
}
