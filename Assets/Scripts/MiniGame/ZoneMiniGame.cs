using MiniGame;
using MushroomMadness.InputSystem;
using MushroomMadness.Player;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Collider))]
public class ZoneMiniGame : MonoBehaviour
{
    [SerializeField] private HandlerLaunchMiniGame _handler;
    [SerializeField] private BlinkText _text;
    [SerializeField] private MiniGameManger _miniGame;

    private bool _isPassed;

    [Inject]
    private IInputMiniGame _input;

    private void Start()
    {
        _isPassed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
            if (!_isPassed)
                TrunOnGame();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
            TrunOffGame();
    }

    private void TrunOnGame()
    {
        _text.Init("ֽאזלטעו ֵ");
        _text.gameObject.SetActive(true);
        _input.ClickStartGame += OnStartGame;
    }

    private void TrunOffGame()
    {
        _text.gameObject.SetActive(false);
        _input.ClickStartGame -= OnStartGame;
    }

    private void OnStartGame()
    {
        _handler.StartGame(_miniGame);
        _text.gameObject.SetActive(false);


        _input.ClickStartGame -= OnStartGame;
        _handler.EndGame += OnEndGame;
    }

    private void OnEndGame(bool isPassed)
    {
        _isPassed = isPassed;

        if (_isPassed)
            TrunOffGame();
        else
        {
            _input.ClickStartGame -= OnStartGame;
            TrunOnGame();
        }
    }
}
