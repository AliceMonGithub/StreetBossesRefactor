using InputService;

namespace Zenject
{
    public class InputInstaller : MonoInstaller
    {
        private MovementInput _input = new StandaloneMovementInput();

        public override void InstallBindings()
        {
            Container.Bind<MovementInput>().FromInstance(_input).AsSingle();
        }
    }
}