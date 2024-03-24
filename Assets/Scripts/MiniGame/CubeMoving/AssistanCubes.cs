using MiniGame.MovingCubes.Config;
using MiniGame.MovingCubes.Cubes;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGame.MovingCubes
{
    public class AssistanCubes : MonoBehaviour, IResetGame
    {
        public IEnumerable<BaseCube> Cubes;
        public IEnumerable<ButtonToggle> Buttons;


        public void Init(ConfigCubes config)
        {
            Buttons = GetComponentsInChildren<ButtonToggle>();
            Cubes = GetCubes();

            foreach (var cube in Cubes)
            {
                cube.Init(config);
            }
        }

        public void Resetting()
        {
            RessettingCubes();

            foreach (var item in Buttons)
            {
                item.Resetting();
            }
        }


        private void RessettingCubes()
        {
            foreach (var item in Cubes)
            {
                item.Resetting();
            }
        }

        private IEnumerable<BaseCube> GetCubes()
        {
            return GetComponentsInChildren<BaseCube>();
        }
    }
}
