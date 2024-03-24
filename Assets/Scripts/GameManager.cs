using MushroomMadness.SceneLoadGame;
using MushroomMadness.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameUIManager _gameUIManager;

    [Space]
    [SerializeField] private List<ZoneMiniGame> _zones;

    [Space]
    [SerializeField] private SceneLoadManager _sceneLoadManager;
    [SerializeField] private int _IdMainMenu = 0;
    [SerializeField] private int _IdNextLevel = 0;

  

    public int CountPassedMiniGame { get; private set; }
    public float TimerGame { get; private set; }


    private void Update()
    {
        TimerGame += Time.deltaTime;
    }

    private void OnEnable()
    {
        _gameUIManager.TransionMainMenu += OnTransionMainMenu;

        foreach (var zone in _zones)
            zone.PassedMiniGame += OnPassedMiniGame;

    }

    private void OnDisable()
    {
        _gameUIManager.TransionMainMenu -= OnTransionMainMenu;

        foreach (var zone in _zones)
            zone.PassedMiniGame -= OnPassedMiniGame;
    }

    private void OnTransionMainMenu()
    {
        _sceneLoadManager.LoadScene(_IdMainMenu);
    }

    private void OnPassedMiniGame()
    {
        CountPassedMiniGame++;
    }
}
