using MiniGame.MovingCubes;
using MiniGame.MovingCubes.Cubes;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGame.MovingCubes
{
    [RequireComponent(typeof(Collider), typeof(MeshRenderer))]
    public class ButtonToggle : MonoBehaviour, IResetGame, IVisible
    {
        [SerializeField] private ConfigButtonToggle _config;

        private SwitchableVisibility _visible;

        private StateVisible _currentVisible;

        public StateVisible StateVisible => _config.StartVisible;

        private void Start()
        {
            var mesh = GetComponent<MeshRenderer>();
            _visible = new SwitchableVisibility(mesh, _config.AlphaTransparency, _config.DurationFade);
            Resetting();
        }

        public void SetVisible(StateVisible state)
        {
            _currentVisible = state;
            ApplyVisible();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerCube playerCube))
                Togle();
        }

        public void Togle()
        {
            _currentVisible = _currentVisible == StateVisible.On ? StateVisible.Off : StateVisible.On;

            ToggleAll();
            ApplyVisible();
        }

        private void ToggleAll()
        {
            SetVisible(_config.CubesOff, _currentVisible);

            SetVisible(_config.ButtonOff, _currentVisible);
        }

        private void SetVisible(IEnumerable<IVisible> visible, StateVisible state)
        {
            foreach (var button in visible)
            {
                button.SetVisible(state);
            }
        }

        public void ApplyVisible()
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

        public void Resetting()
        {
            _currentVisible = _config.StartVisible;
            ApplyVisible();
        }

        private void OnValidate()
        {
            GetComponent<Collider>().isTrigger = true;
        }
    }
}

[Serializable]
public class ConfigButtonToggle
{
    [field: SerializeField] public List<SwitchableCube> CubesOff { get; private set; }
    [field: SerializeField] public List<ButtonToggle> ButtonOff { get; private set; }

    [field: SerializeField] public readonly StateVisible StartVisible = StateVisible.On;

    public readonly float AlphaTransparency = 0.3f;

    public readonly float DurationFade = 0.4f;
}