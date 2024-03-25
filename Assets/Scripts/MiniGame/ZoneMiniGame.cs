using MiniGame;
using MiniGame.Factory;
using MiniGame.Handlers;
using MushroomMadness.InputSystem;
using MushroomMadness.Player;
using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Collider))]
public class ZoneMiniGame : MonoBehaviour
{
    [SerializeField] private MiniGameManger _miniGame;
    [SerializeField] private Blockage _blockage;

    [SerializeField] private BlinkText _text;
    [SerializeField] private HandlerLaunchMiniGame _launcher;
    [SerializeField] private FactoryMiniGame _factory;
    
    [Inject]
    private IInputMiniGame _input;

    public bool IsPassed;

    public event Action PassedMiniGame;


    private void Start()
    {
        IsPassed = false;
        _miniGame = _factory.GetNewInstansMiniGame(_miniGame, _launcher.GetContenerMiniGame());
        _miniGame.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
            if (!IsPassed)
                TrunOnViewGame();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
            TrunOffViewGame();
    }

    private void TrunOnViewGame()
    {
        _text.Init("ֽאזלטעו ֵ");
        _text.gameObject.SetActive(true);
        _input.ClickStartGame += OnStartGame;
    }

    private void TrunOffViewGame()
    {
        _text.gameObject.SetActive(false);
        _input.ClickStartGame -= OnStartGame;
    }

    private void OnStartGame()
    {
        TrunOffViewGame();
        _launcher.StartGame(_miniGame);
        _launcher.EndGame += OnEndGame;
    }

    private void OnEndGame(bool isPassed)
    {
        IsPassed = isPassed;

        if (IsPassed)
        {
            TrunOffViewGame();
            PassedMiniGame?.Invoke();
            _blockage.ExplodePassege();
        }
        else
        {
            _input.ClickStartGame -= OnStartGame;
            TrunOnViewGame();
        }

        _launcher.EndGame += OnEndGame;
    }
}
