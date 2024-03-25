using MushroomMadness.InputSystem;
using MushroomMadness.Player;
using MushroomMadness.SceneLoadGame;
using MushroomMadness.UI;
using UnityEngine;
using Zenject;

public class PauseHandler : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GameUIManager _gameUIManager;
    [SerializeField] private SceneLoadManager _sceneLoadManager;

    [Inject]
    private IInputMove _inputMove;

    private const float timeContinie = 1f;
    private const float timePause = 0f;

    private void OnEnable()
    {
        _gameUIManager.PauseOpen += SetPause;
        _sceneLoadManager.ReadySceneLoad += ReadySceneLoad;
        _gameUIManager.VictoryOpen += SetPause;
    }

    private void OnDisable()
    {
        _gameUIManager.PauseOpen -= SetPause;
        _sceneLoadManager.ReadySceneLoad -= ReadySceneLoad;
        Time.timeScale = timeContinie;
    }

    public void SetPause(bool isPause)
    {
        if (isPause)
            Time.timeScale = timePause;

        else if (!isPause)
            Time.timeScale = timeContinie;

        SetActiveObjects(!isPause);
    }

    private void SetActiveObjects(bool active)
    {
        _player.SetActiveMove(active);
        _inputMove.SetActiveMove(active);
    }

    private void ReadySceneLoad()
    {
        SetActiveObjects(true);
    }
}
