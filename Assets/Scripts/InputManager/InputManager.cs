using System;
using UnityEngine;

namespace MushroomMadness.InputSystem
{
    public class InputManager : MonoBehaviour, IInputMove
    {
        private MainInputMap _mainInputMap;
        public event Action<bool> ClickMove;
        public event Action ClickJump;

        private void OnEnable()
        {
            _mainInputMap.Enable();

            _mainInputMap.Player.Move.started += stx => ClickMove.Invoke(true);
            _mainInputMap.Player.Move.canceled += stx => ClickMove.Invoke(false);

            _mainInputMap.Player.Jump.performed += stx => ClickJump.Invoke();
        }

        private void OnDisable()
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
    }
}
