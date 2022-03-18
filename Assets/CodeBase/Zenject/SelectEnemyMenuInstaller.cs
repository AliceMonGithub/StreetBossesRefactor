using Assets.CodeBase;
using System.Collections;
using UnityEngine;

namespace Zenject
{
    public class SelectEnemyMenuInstaller : MonoInstaller
    {
        [SerializeField] private SelectEnemyMenu _menu;

        public SelectEnemyMenu Menu => _menu;

        public override void InstallBindings()
        {
            Container.Bind<SelectEnemyMenu>().FromInstance(_menu);
        }
    }
}