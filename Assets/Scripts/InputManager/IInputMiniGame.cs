using System;

namespace MushroomMadness.InputSystem
{
    public interface IInputMiniGame
    {
        public event Action ClickResetGame;
        public event Action ClickExitGame;
        public event Action ClickStartGame;
    }
}