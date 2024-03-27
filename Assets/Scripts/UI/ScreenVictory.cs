using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MushroomMadness.UI
{
    public class ScreenVictory : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _labelButton;

        [SerializeField] private TMP_Text _labelPassedGame;
        [SerializeField] private TMP_Text _labelTimeGame;

        private const float secondInMinut = 60f;

        public event Action ClickButton;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        private void OnClick() => ClickButton?.Invoke();

        public void SetInfo(int passedGame, int maxGame, float TimeGameSecond, string textButton)
        {
            TimeSpan time = TimeSpan.FromSeconds(TimeGameSecond);

            _labelTimeGame.text = time.ToString("hh':'mm':'ss");
            _labelPassedGame.text = passedGame.ToString() + '/' + maxGame.ToString();

            _labelButton.text = textButton;
        }
    }
}
