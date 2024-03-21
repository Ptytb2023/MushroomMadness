using MiniGame;
using MushroomMadness.Controllers;
using MushroomMadness.InputSystem;
using System;
using UnityEngine;
using Zenject;

public class HandlerLaunchMiniGame : MonoBehaviour
{
    [SerializeField] private MovePlayerConroller _playerController;
    [SerializeField] private Camera _cameraMiniGame;
    [SerializeField] private Transform _pointSpawn;
    [SerializeField] private Canvas _bagroundCanvas;

    [Inject]
    private InputManager _input;

    private MiniGameManger _currentMiniGame;

    public event Action<bool> EndGame;

    public void StartGame(MiniGameManger miniGame)
    {
        _cameraMiniGame.gameObject.SetActive(true);
        _bagroundCanvas.gameObject.SetActive(true);

        var game = Instantiate(miniGame, _pointSpawn);
        game.InitGame(_input);
        game.transform.position = _pointSpawn.position;

        _currentMiniGame = game;

        _playerController.enabled = false;

        game.EndGame += ExitGame;
        _input.ClickExitGame += OnClickExitGame;
        _input.ClickResetGame += OnResetGame;

    }
    private void OnClickExitGame() => ExitGame(false);

    private void OnResetGame()
    {
        _currentMiniGame?.ResetGame();
    }

    private void ExitGame(bool isSuccessfully)
    {
        Destroy(_currentMiniGame.gameObject);
        _cameraMiniGame.gameObject.SetActive(false);
        _bagroundCanvas.gameObject.SetActive(false);

        EndGame?.Invoke(isSuccessfully);
        _currentMiniGame.EndGame -= ExitGame;
        _input.ClickExitGame -= OnClickExitGame;
        _input.ClickResetGame -= OnResetGame;

        _playerController.enabled = true;
    }
}
