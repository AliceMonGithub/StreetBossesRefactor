using Assets.CodeBase.SkillMenu;
using UnityEngine;
using Zenject;

namespace CodeBase.Zenject
{
    public class SkillBehaviorInstaller : MonoInstaller
    {
        [SerializeField] private SkillBehavior _skillBehavior;

        public override void InstallBindings()
        {
            Container.Bind<SkillBehavior>().FromInstance(_skillBehavior);
        }
    }
}