using Assets.CodeBase;
using CodeBase;
using SceneLogic;
using UltEvents;
using UnityEngine;
using Zenject;

public class SceneLoaderInstaller : MonoInstaller
{
    [SerializeField] private UltEvent _onHideEvent;

    private LoadCurtain _loadCurtain;

    public override void InstallBindings()
    {
        _loadCurtain = FindObjectOfType<LoadCurtain>();

        _loadCurtain.OnHideEvent += _onHideEvent.Invoke;

        Container.Bind<LoadCurtain>().FromInstance(_loadCurtain).AsSingle();
    }
}