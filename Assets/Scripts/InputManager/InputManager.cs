using System;
using UnityEngine;

namespace MushroomMadness.InputSystem
{
    public class InputManager : IInputMove, IInputMiniGame, IinputInterface, IDisposable
    {
        private MainInputMap _mainInputMap;

        public event Action<bool> ClickMove;
        public event Action ClickJump;

        public event Action ClickResetGame;
        public event Action ClickExitGame;
        public event Action ClickStartGame;
        public event Action ClickMap;

        public MainInputMap InputMap => _mainInputMap;

        private bool _enabled;

        public InputManager()
        {
            _mainInputMap = new MainInputMap();
            TurnOn();
        }

        public void SetActive(bool active)
        {
            if (active)
                TurnOn();
            else if (!active)
                TurnOff();
        }


        private void TurnOn()
        {
            if (_enabled) return;

            _enabled = true;

            _mainInputMap.Enable();

            SetActiveMove(true);

            _mainInputMap.MiniGame.ResetGame.performed += stx => ClickResetGame?.Invoke();
            _mainInputMap.MiniGame.ExitGame.performed += stx => ClickExitGame?.Invoke();
            _mainInputMap.MiniGame.StartGame.performed += stx => ClickStartGame?.Invoke();

        }


        private void TurnOff()
        {
            if (!_enabled) return;

            _enabled = false;

            _mainInputMap.Disable();

            SetActiveMove(false);

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
            TurnOff();
        }

        public void SetActiveMove(bool active)
        {
            if (!active)
            {
                _mainInputMap.Player.Move.started -= stx => ClickMove.Invoke(true);
                _mainInputMap.Player.Move.canceled -= stx => ClickMove.Invoke(false);

                _mainInputMap.Player.Jump.performed -= stx => ClickJump.Invoke();

                _mainInputMap.Player.Map.performed -= stx => ClickMap?.Invoke();
            }
            else if (active)
            {
                _mainInputMap.Player.Move.started += stx => ClickMove?.Invoke(true);
                _mainInputMap.Player.Move.canceled += stx => ClickMove?.Invoke(false);

                _mainInputMap.Player.Jump.performed += stx => ClickJump?.Invoke();

                _mainInputMap.Player.Map.performed += stx => ClickMap?.Invoke();
            }
        }
    }
}
