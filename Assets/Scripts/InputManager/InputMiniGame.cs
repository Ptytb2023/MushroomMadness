using MushroomMadness.InputSystem;
using System;

namespace Assets.Scripts.InputManager
{
    internal class InputMiniGame : IInputMiniGame
    {
        public event Action ClickResetGame;
        public event Action ClickExitGame;
        public event Action ClickStartGame;
    }
}
