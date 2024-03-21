using MushroomMadness.InputSystem;
using Zenject;

namespace MushroomMadness.Instalers
{
    public class InputMonoInstaler : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<InputManager>().AsSingle();
        }
    }
}