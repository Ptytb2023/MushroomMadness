using MiniGame.MovingCubes.Cube;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGame.MovingCubes
{
    public class ContenerCubs : MonoBehaviour
    {
        public IEnumerable<CubeMoving> GetCubes()
        {
           return GetComponentsInChildren<CubeMoving>();
        }
    }
}
