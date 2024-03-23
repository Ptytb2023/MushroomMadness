using MiniGame.MovingCubes.Config;
using MiniGame.MovingCubes.Controller;
using System;
using UnityEngine;

namespace MiniGame.MovingCubes
{
    [RequireComponent(typeof(MoveCubesController))]
    public class CubeMovingManagerGame : MiniGameManger
    {
        [SerializeField] private AssistanCubes _assistan;
        [SerializeField] private FinishZone _finish;
        [SerializeField] private ConfigCubes _config;

        private MoveCubesController _controler;

        public override event Action<bool> EndGame;

        private void OnEnable()
        {
            _finish.EndGame += OnEndGame;

            _controler = GetComponent<MoveCubesController>();
            _assistan.Init(_config);
            _controler.Init(_assistan);
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
            _assistan.Resetting();
        }
    }
}
