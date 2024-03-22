using MiniGame.MovingCubes.Cubes;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGame.MovingCubes
{
    [RequireComponent(typeof(Collider), typeof(MeshRenderer))]
    public class ButtonToggle : MonoBehaviour, IResetGame
    {
        [SerializeField] private List<SwitchableCube> _cubesCouple;
        [SerializeField] private ButtonToggle _buttonCouple;
        [SerializeField] private StateVisible _startVisible;

        private SwitchableVisibility _visible;
        private StateVisible _currentVisible;


        private const float _alphaTransparency = 0.4f;
        private const float _durationFade = 0.3f;

        public StateVisible StateVisible => _startVisible;

        private void Start()
        {
            var mesh = GetComponent<MeshRenderer>();
            _currentVisible = _startVisible;
            _visible = new SwitchableVisibility(mesh, _alphaTransparency, _durationFade);

            SetStateVisiblForCubes();
            ApplyVisibly();
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerCube playerCube))
                Toggle();
        }

        private void Toggle()
        {
            _currentVisible = _currentVisible == StateVisible.On ? StateVisible.Off : StateVisible.On;

            ApplyVisibly();
            ToggleCubes();
        }

        private void ApplyVisibly()
        {
            switch (_currentVisible)
            {
                case StateVisible.Off:
                    _visible.SetActive(false);
                    break;

                case StateVisible.On:
                    _visible.SetActive(true);
                    break;
            }
        }

        private void ToggleCubes()
        {
            foreach (var cube in _cubesCouple)
            {
                cube.Toggle();
            }
        }

        private void SetStateVisiblForCubes()
        {
            StateVisible state = _currentVisible == StateVisible.On ? StateVisible.Off : StateVisible.On;

            foreach (var cube in _cubesCouple)
            {
                cube.SetStateVisible(state);
            }
        }


        private void OnValidate()
        {
            GetComponent<Collider>().isTrigger = true;

            if (_buttonCouple != null)
                if (_buttonCouple.StateVisible == _currentVisible)
                    Debug.LogError($"{_buttonCouple} It has the same starting state");
        }

        public void Resetting()
        {
            _currentVisible = _startVisible;
            ApplyVisibly();
        }
    }
}