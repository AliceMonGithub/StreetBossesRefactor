using Assets.CodeBase;
using CodeBase;
using SceneLogic;
using UltEvents;
using UnityEngine;
using Zenject;

public class SceneLoaderInstaller : MonoInstaller
{
    [SerializeField] private UltEvent _onHideEvent;

    [SerializeField] private LoadCurtain _loadCurtain;

    public override void InstallBindings()
    {
        if(_loadCurtain == null)
        {
            var loadCurtain = FindObjectOfType<LoadCurtain>();

            loadCurtain.OnHideEvent += _onHideEvent.Invoke;

            Container.Bind<LoadCurtain>().FromInstance(loadCurtain).AsSingle();
        }
        else
        {
            Container.Bind<LoadCurtain>().FromInstance(_loadCurtain).AsSingle();
        }
    }
}