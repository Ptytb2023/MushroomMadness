using MushroomMadness.UI.Buttons;
using System;
using UnityEngine;

namespace MushroomMadness.UI.Pause
{
    public class ScreenPause : MonoBehaviour
    {
        [SerializeField] private ButtonContinue _buttonContinue;
        [SerializeField] private ButtonExitGame _buttonExitGame;

        public Action ExitInMainMenu;
        public Action ContinueGame;

        private void OnEnable()
        {
            _buttonContinue.OnButtonClick += OnClickContinioGame;
            _buttonExitGame.OnButtonClick += OnClickExitGame;
        }

        private void OnDisable()
        {
            _buttonContinue.OnButtonClick -= OnClickContinioGame;
            _buttonExitGame.OnButtonClick -= OnClickExitGame;
        }

        private void OnClickExitGame() => ExitInMainMenu?.Invoke();
        private void OnClickContinioGame() => ContinueGame?.Invoke();
    }
}