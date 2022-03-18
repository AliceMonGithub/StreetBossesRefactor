using SceneLogic;
using UltEvents;
using UnityEngine;
using Zenject;

internal class FinishMenu : MonoBehaviour
{
    [SerializeField] private UltEvent _onShow;

    private LoadCurtain _curtain;

    [Inject]
    private void Construct(LoadCurtain curtain)
    {
        _curtain = curtain;
    }

    public void Show()
    {
        _onShow.Invoke();
    }

    public void Exit()
    {
        _curtain.LoadScene("FirstStreet");
    }

    public void RestartBattle()
    {
        _curtain.LoadScene(_curtain.CurrentSceneName);
    }
}