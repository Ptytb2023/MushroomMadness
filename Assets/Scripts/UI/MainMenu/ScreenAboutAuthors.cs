using MushroomMadness.InputSystem;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace MushroomMadness.UI
{
    public class ScreenAboutAuthors : MonoBehaviour
    {
        [SerializeField] private List<ButtonURl> _buttonsURl;

        public Action ClosePanel;

        [Inject]
        private IInputMiniGame _input;

        private void OnEnable()
        {
            foreach (var buttonURL in _buttonsURl)
            {
                buttonURL.EnableButton();
                buttonURL.ButtonClick += OnClickButton;
            }

            _input.ClickExitGame += OnClickClose;
        }

        private void OnDisable()
        {
            foreach (var buttonURL in _buttonsURl)
            {
                buttonURL.ButtonClick -= OnClickButton;
                buttonURL.DisableButton();
            }

            _input.ClickExitGame -= OnClickClose;
        }

        private void OnClickButton(ButtonURl buttonURl)
        {
            var url = buttonURl.URL;

            Application.OpenURL(url);
        }

        private void OnClickClose()
        {
            ClosePanel?.Invoke();
        }
    }

    [Serializable]
    public class ButtonURl : IDisposable
    {
        [field: SerializeField] public ButtonURLView View;
        [field: SerializeField] public string URL { get; private set; }
        [field: SerializeField] public string NameButton { get; private set; }
        [field: SerializeField] public string Description { get; private set; }

        public Action<ButtonURl> ButtonClick;

        public void EnableButton()
        {
            View.gameObject.SetActive(true);
            View.ButtonURlClick.onClick.AddListener(OnClick);
            View.NameButton.text = NameButton;
            View.Description.text = Description;
        }

        public void DisableButton()
        {
            View.gameObject.SetActive(false);
            View.ButtonURlClick.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            ButtonClick?.Invoke(this);
        }

        public void Dispose()
        {
            View.ButtonURlClick.onClick.RemoveListener(OnClick);
        }
    }
}
