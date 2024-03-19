using MushroomMadness.InputSystem;
using Zenject;

namespace MushroomMadness.Instalers
{
    public class PlayerMonoInstaler : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IInputMove>().To<InputManager>().FromNew().AsSingle();
        }
    }
}