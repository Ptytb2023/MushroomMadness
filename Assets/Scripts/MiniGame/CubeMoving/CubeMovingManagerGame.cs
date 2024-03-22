using System;
using UnityEngine;

namespace MiniGame.MovingCubes
{
    public class CubeMovingManagerGame : MiniGameManger
    {
        [SerializeField] private CubeSaver _saver;
        [SerializeField] private FinishZone _finish;

        public override event Action<bool> EndGame;

        private void OnEnable()
        {
            _finish.EndGame += OnEndGame;
        }

        private void OnDisable()
        {
            _finish.EndGame -= OnEndGame;
        }

        private void OnEndGame()
        {
            EndGame?.Invoke(true);
        }

        public override void ResetGame()
        {
           _saver.ResetField();
        }
    }
}
