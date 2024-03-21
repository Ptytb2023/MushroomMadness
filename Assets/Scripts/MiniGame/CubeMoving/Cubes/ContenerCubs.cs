using MiniGame.CubesMoving.Cube;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGame.CubesMoving
{
    public class ContenerCubs : MonoBehaviour
    {
        public IEnumerable<CubeMoving> GetCubes()
        {
           return GetComponentsInChildren<CubeMoving>();
        }
    }
}
