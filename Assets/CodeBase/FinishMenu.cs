using CodeBase;
using SceneLogic;
using UltEvents;
using UnityEngine;
using Zenject;

internal class FinishMenu : MonoBehaviour
{
    [SerializeField] private UltEvent _onShow;

    [SerializeField] private PlayerStats _playerStats;

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
        _curtain.LoadScene(_playerStats.LastSceneName);
    }

    public void RestartBattle()
    {
        _curtain.LoadScene(_curtain.CurrentSceneName);
    }
}