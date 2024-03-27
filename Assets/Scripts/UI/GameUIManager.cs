using MushroomMadness.InputSystem;
using MushroomMadness.UI.LoadScene;
using MushroomMadness.UI.Pause;
using System;
using UnityEngine;
using Zenject;

namespace MushroomMadness.UI
{
    public class GameUIManager : MonoBehaviour
    {
        [SerializeField] private ScreenVictory _victory;
        [SerializeField] private ScreenLoadGame _loadGame;
        [SerializeField] private ScreenPause _pause;

        [Inject]
        private IInputMiniGame _miniGame;

        private const string _nextLevel = "Следующии уровень";
        private const string _backMenuGame = "Назад в Меню";

        public event Action<bool> PauseOpen;
        public event Action<bool> VictoryOpen;
        public event Action TransionMainMenu;
        public event Action ClickButtonVictory;

        private void OnEnable()
        {
            _miniGame.ClickExitGame += OnClickEscape;
        }

        private void OnDisable()
        {
            _miniGame.ClickExitGame -= OnClickEscape;
        }


        private void OnClickEscape()
        {
            if (_pause.gameObject.activeSelf)
            {
                _pause.ExitInMainMenu -= OnExitInMainMenu;
                _pause.ContinueGame -= OnContinieGame;
                SetPauseScreen(false);
                return;
            }

            _pause.ContinueGame += OnContinieGame;
            _pause.ExitInMainMenu += OnExitInMainMenu;

            SetPauseScreen(true);
        }

        private void SetPauseScreen(bool isPauss)
        {
            _pause.gameObject.SetActive(isPauss);
            PauseOpen?.Invoke(isPauss);
        }

        private void OnContinieGame()
        {
            OnClickEscape();
        }

        private void OnExitInMainMenu()
        {
            _pause.ExitInMainMenu -= OnExitInMainMenu;
            _pause.ContinueGame -= OnContinieGame;
            _loadGame.gameObject.SetActive(true);

            TransionMainMenu?.Invoke();
        }

        public void ShowScreenVictory(int passedGame, int maxGame, float TimeGameSecond, bool isNexLevel)
        {
            string textButton;

            if (isNexLevel)
                textButton = _nextLevel;
            else
                textButton = _backMenuGame;

            _victory.gameObject.SetActive(true);
            _victory.SetInfo(passedGame, maxGame, TimeGameSecond, textButton);

            _victory.ClickButton += OnClickButtonScreenVictory;
            _miniGame.ClickExitGame -= OnClickEscape;

            VictoryOpen?.Invoke(true);
        }

        private void OnClickButtonScreenVictory()
        {
            _victory.gameObject.SetActive(false);
            _loadGame.gameObject.SetActive(true);
            ClickButtonVictory?.Invoke();
        }
    }
}
