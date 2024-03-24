using MushroomMadness.Controllers;
using MushroomMadness.InputSystem;
using MushroomMadness.Player;
using System;
using UnityEngine;
using Zenject;

namespace MiniGame.Handlers
{
    public class HandlerLaunchMiniGame : MonoBehaviour, IHandlerLaunch
    {
        [SerializeField] private Player _player;
        [SerializeField] private Camera _cameraMiniGame;
        [SerializeField] private Transform _pointSpawn;
        [SerializeField] private Canvas _bagroundCanvas;

        [Inject]
        private IInputMiniGame _input;


        private MiniGameManger _currentMiniGame;

        public event Action<bool> EndGame;

        public void StartGame(MiniGameManger miniGame)
        {
            _currentMiniGame = miniGame;
            SetActiveInspectCamera(true);

            _player.OffMoveController();

            _currentMiniGame.EndGame += ExitGame;
            _input.ClickExitGame += OnClickExitGame;
            _input.ClickResetGame += OnClickResetGame;

        }

        private void SetActiveInspectCamera(bool active)
        {
            _currentMiniGame.gameObject.SetActive(active);
            _cameraMiniGame.gameObject.SetActive(active);
            _bagroundCanvas.gameObject.SetActive(active);

            if (active)
                _currentMiniGame.transform.position = _pointSpawn.position;
        }

        private void OnClickExitGame() => ExitGame(false);

        private void OnClickResetGame() => _currentMiniGame?.ResetGame();


        private void ExitGame(bool isSuccessfully)
        {
            SetActiveInspectCamera(false);
            EndGame?.Invoke(isSuccessfully);


            _currentMiniGame.EndGame -= ExitGame;
            _input.ClickExitGame -= OnClickExitGame;
            _input.ClickResetGame -= OnClickResetGame;

            _player.OnMoveController();
        }

        public Transform GetContenerMiniGame() => _pointSpawn;
    }
}
