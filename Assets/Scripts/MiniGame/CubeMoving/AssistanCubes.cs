using MiniGame.MovingCubes.Config;
using MiniGame.MovingCubes.Cubes;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGame.MovingCubes
{
    public class AssistanCubes : MonoBehaviour, IResetGame
    {
        public IEnumerable<BaseCube> Cubes;

        private IEnumerable<IResetGame> _resetting;

        public void Init(ConfigCubes config)
        {
            _resetting = GetComponentsInChildren<IResetGame>();

            Cubes = GetCubes();

            foreach (var cube in Cubes)
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
    }
}
