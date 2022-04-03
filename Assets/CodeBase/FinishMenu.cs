using Assets.CodeBase.SkillMenu;
using CodeBase;
using SceneLogic;
using UltEvents;
using UnityEngine;
using Zenject;

internal class FinishMenu : MonoBehaviour
{
    [SerializeField] private UltEvent _onShow;

    [SerializeField] private PlayerStats _playerStats;

    [SerializeField] private SkillBehavior _skillBehavior;

    private LoadCurtain _curtain;

    [Inject]
    private void Construct(LoadCurtain curtain)
    {
        _curtain = curtain;
    }

    private void OnEnable()
    {
        _skillBehavior.Enabled = false;
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