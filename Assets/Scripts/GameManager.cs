using MushroomMadness.Player;
using MushroomMadness.SceneLoadGame;
using MushroomMadness.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player _player;

    [Space]
    [SerializeField] private GameUIManager _gameUIManager;

    [Space]
    [SerializeField] private List<ZoneMiniGame> _zones;

    [Space]
    [SerializeField] private SceneLoadManager _sceneLoadManager;
    [SerializeField] private int _IdMainMenu = 0;
    [SerializeField][Min(-1)] private int _IdNextLevel = -1;

    [Space]
    [SerializeField] private MushroomMadness.Zone.FinishZone _finishZone;

    public int CountPassedMiniGame { get; private set; }
    public float TimeGame { get; private set; }

    private Coroutine _timer;

    private void Start()
    {
        _timer = StartCoroutine(TimerUpdate());
    }

    private void OnEnable()
    {
        _gameUIManager.TransionMainMenu += OnTransionMainMenu;

        foreach (var zone in _zones)
            zone.PassedMiniGame += OnPassedMiniGame;

        _finishZone.PlayerReachedFinish += EndGame;
    }

    private void OnDisable()
    {
        _gameUIManager.TransionMainMenu -= OnTransionMainMenu;

        foreach (var zone in _zones)
            zone.PassedMiniGame -= OnPassedMiniGame;

        _finishZone.PlayerReachedFinish -= EndGame;
    }

    private void OnTransionMainMenu()
    {
        _sceneLoadManager.LoadScene(_IdMainMenu);
    }

    private void OnPassedMiniGame()
    {
        CountPassedMiniGame++;
    }

    private IEnumerator TimerUpdate()
    {
        while (enabled && Time.timeScale > 0)
        {
            TimeGame++;
            yield return new WaitForSeconds(1);
        }
    }

    private void EndGame()
    {
        bool isNextLevel = _IdNextLevel > 0;
        StopCoroutine(_timer);

        _gameUIManager.ShowScreenVictory(CountPassedMiniGame, _zones.Count, TimeGame, isNextLevel);

        _gameUIManager.ClickButtonVictory += OnClickButtonVictory;
    }

    private void OnClickButtonVictory()
    {
        int idScene = _IdNextLevel > 0 ? _IdNextLevel : _IdMainMenu;
        _sceneLoadManager.LoadScene(idScene);
    }
}
