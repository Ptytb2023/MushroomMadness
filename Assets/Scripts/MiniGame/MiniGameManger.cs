using MushroomMadness.InputSystem;
using System;
using UnityEngine;

namespace MiniGame
{
    public abstract class MiniGameManger : MonoBehaviour
    {
        public abstract event Action<bool> EndGame;
        public abstract void ResetGame();

    }
}