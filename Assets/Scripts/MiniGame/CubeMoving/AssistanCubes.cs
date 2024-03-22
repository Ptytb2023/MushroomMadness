using MiniGame.MovingCubes.Config;
using MiniGame.MovingCubes.Cubes;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGame.MovingCubes
{
    public class AssistanCubes : MonoBehaviour, IResetGame
    {
        public IEnumerable<BaseCube> _cubes;
        public IEnumerable<ButtonToggle> _buttons;

        private IEnumerable<IResetGame> _resetting;

        public void Init(ConfigCubes config)
        {
            _cubes = GetCubes();
            _buttons = GetButton();

            _resetting = GetComponentsInChildren<IResetGame>();

            foreach (var cube in _cubes)
            {
                cube.Init(config);
            }
        }

        public void Resetting()
        {
            foreach (var item in _resetting)
            {
                item.Resetting();
            }

        }

        private IEnumerable<BaseCube> GetCubes()
        {
            return GetComponentsInChildren<BaseCube>();
        }

        private IEnumerable<ButtonToggle> GetButton()
        {
            return GetComponentsInChildren<ButtonToggle>();
        }
    }
}
