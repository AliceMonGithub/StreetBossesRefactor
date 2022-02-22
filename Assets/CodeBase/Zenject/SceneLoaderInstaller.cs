using Assets.CodeBase.UILogic;
using CodeBase;
using UnityEngine;
using Zenject;

public class SceneLoaderInstaller : MonoInstaller
{
    [SerializeField] private LoadCurtain _loadCurtain;

    private SceneLoader _sceneLoader;


    public override void InstallBindings()
    {
        _sceneLoader = new SceneLoader(_loadCurtain);

        Container.Bind<SceneLoader>().FromInstance(_sceneLoader).AsSingle();
    }
}