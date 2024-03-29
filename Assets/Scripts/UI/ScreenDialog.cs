using MushroomMadness.InputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MushroomMadness.UI
{
    public class ScreenDialog : MonoBehaviour
    {
        [SerializeField] private Button _button;

        [Inject]
        private InputManager _inputManager;

        private void OnEnable()
        {
            _inputManager.SetActive(false);
            _button.onClick.AddListener(OnClickButton);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClickButton);
        }

        private void OnClickButton()
        {
            _inputManager?.SetActive(true);

            gameObject.SetActive(false);
        }
    }
}