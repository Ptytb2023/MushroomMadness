using MiniGame;
using MiniGame.Factory;
using MiniGame.Handlers;
using MushroomMadness.InputSystem;
using MushroomMadness.Player;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Collider))]
public class ZoneMiniGame : MonoBehaviour
{
    [SerializeField] private MiniGameManger _miniGame;

    [SerializeField] private BlinkText _text;
    [SerializeField] private HandlerLaunchMiniGame _launcher;
    [SerializeField] private FactoryMiniGame _factory;
    
    [Inject]
    private IInputMiniGame _input;

    private bool _isPassed;

    public MiniGameManger MiniGame => _miniGame;

    private void Start()
    {
        _isPassed = false;
        _miniGame = _factory.GetNewInstansMiniGame(_miniGame, _launcher.GetContenerMiniGame());
        _miniGame.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
            if (!_isPassed)
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
        _isPassed = isPassed;

        if (_isPassed)
            TrunOffViewGame();
        else
        {
            _input.ClickStartGame -= OnStartGame;
            TrunOnViewGame();
        }

        _launcher.EndGame += OnEndGame;
    }
}
