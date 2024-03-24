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
        [SerializeField] private ScreenLoadGame _loadGame;
        [SerializeField] private ScreenPause _pause;

        [Inject]
        private IInputMiniGame _miniGame;

        public event Action<bool> PauseOpen;
        public event Action TransionMainMenu;

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
                SetPause(false);
                return;
            }

            _pause.ContinueGame += OnContinieGame;
            _pause.ExitInMainMenu += OnExitInMainMenu;

            SetPause(true);
        }

        private void SetPause(bool isPauss)
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
    }
}
