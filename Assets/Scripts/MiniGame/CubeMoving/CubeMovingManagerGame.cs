using MiniGame.CubesMoving.Controller;
using MushroomMadness.InputSystem;
using System;
using UnityEngine;

namespace MiniGame.CubesMoving
{
    public class CubeMovingManagerGame : MiniGameManger
    {
        [SerializeField] private FieldGame _field;
        [SerializeField] private MoveCubesController _controller;
        [SerializeField] private FinishZone _finish;

        public override event Action<bool> EndGame;

        private void OnDestroy()
        {
            _finish.EndGame -= OnEndGame;
        }

        private void OnEndGame()
        {
            EndGame?.Invoke(true);
        }

        public override void ResetGame()
        {
           _field.ResetField();
        }

        public override void InitGame(InputManager input)
        {
            _controller.Init(input);
            _finish.EndGame += OnEndGame;
        }
    }
}
