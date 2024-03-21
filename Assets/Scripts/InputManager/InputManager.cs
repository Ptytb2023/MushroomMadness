using System;
using UnityEngine;

namespace MushroomMadness.InputSystem
{
    public class InputManager : IInputMove, IInputMiniGame, IDisposable
    {
        private MainInputMap _mainInputMap;

        public event Action<bool> ClickMove;
        public event Action ClickJump;

        public event Action ClickResetGame;
        public event Action ClickExitGame;
        public event Action ClickStartGame;

        public MainInputMap InputMap => _mainInputMap;

        public InputManager()
        {
            _mainInputMap = new MainInputMap();

            _mainInputMap.Enable();

            _mainInputMap.Player.Move.started += stx => ClickMove?.Invoke(true);
            _mainInputMap.Player.Move.canceled += stx => ClickMove?.Invoke(false);

            _mainInputMap.Player.Jump.performed += stx => ClickJump?.Invoke();

            _mainInputMap.MiniGame.ResetGame.performed += stx => ClickResetGame?.Invoke();
            _mainInputMap.MiniGame.ExitGame.performed += stx => ClickExitGame?.Invoke();
            _mainInputMap.MiniGame.StartGame.performed += stx => ClickStartGame?.Invoke();   
        }

        public void OffInputManager()
        {
            _mainInputMap.Disable();

            _mainInputMap.Player.Move.started -= stx => ClickMove.Invoke(true);
            _mainInputMap.Player.Move.canceled -= stx => ClickMove.Invoke(false);

            _mainInputMap.Player.Jump.performed -= stx => ClickJump.Invoke();

            _mainInputMap.MiniGame.ResetGame.performed -= stx => ClickResetGame.Invoke();
            _mainInputMap.MiniGame.ExitGame.performed -= stx => ClickExitGame.Invoke();
            _mainInputMap.MiniGame.StartGame.performed -= stx => ClickStartGame.Invoke();
        }

        public Vector2 GetDirectionMove()
        {
            return _mainInputMap.Player.Move.ReadValue<Vector2>();
        }

        public void Dispose()
        {
            OffInputManager();
        }
    }
}
