using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MushroomMadness.UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public class BaseButton : MonoBehaviour
    {
        [field: SerializeField] protected TMP_Text Label { get; private set; }

        [field: SerializeField] protected Button Button { get; private set; }

        public event Action OnButtonClick;


        protected virtual void OnEnable()
        {
            Button.onClick.AddListener(Onclick);
        }

        protected virtual void OnDisable()
        {
            Button.onClick.RemoveListener(Onclick);
        }

        private void Onclick() => OnButtonClick?.Invoke();

    }
}
