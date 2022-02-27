using Assets.CodeBase;
using CodeBase;
using SceneLogic;
using UnityEngine;
using Zenject;

public class SceneLoaderInstaller : MonoInstaller
{
    private LoadCurtain _loadCurtain;

    public override void InstallBindings()
    {
        _loadCurtain = FindObjectOfType<LoadCurtain>();

        Container.Bind<LoadCurtain>().FromInstance(_loadCurtain).AsSingle();
    }
}