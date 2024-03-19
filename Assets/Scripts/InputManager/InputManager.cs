using System;
using UnityEngine;

namespace MushroomMadness.InputSystem
{
    public class InputManager : IInputMove, IDisposable
    {
        private MainInputMap _mainInputMap;
        public event Action<bool> ClickMove;
        public event Action ClickJump;

        public InputManager()
        {
            _mainInputMap = new MainInputMap();

            _mainInputMap.Enable();

            _mainInputMap.Player.Move.started += stx => ClickMove.Invoke(true);
            _mainInputMap.Player.Move.canceled += stx => ClickMove.Invoke(false);

            _mainInputMap.Player.Jump.performed += stx => ClickJump.Invoke();
        }

        public void OffInputManager()
        {
            _mainInputMap.Disable();

            _mainInputMap.Player.Move.started -= stx => ClickMove.Invoke(true);
            _mainInputMap.Player.Move.canceled -= stx => ClickMove.Invoke(false);

            _mainInputMap.Player.Jump.performed -= stx => ClickJump.Invoke();
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
