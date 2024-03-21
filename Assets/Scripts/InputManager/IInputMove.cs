using System;
using UnityEngine;

namespace MushroomMadness.InputSystem
{
    public interface IInputMove
    {
        public event Action ClickJump;

        public event Action<bool> ClickMove;
        public Vector2 GetDirectionMove();
    }
}