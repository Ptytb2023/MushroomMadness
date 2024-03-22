using MiniGame.MovingCubes.Cubes;
using System;
using UnityEngine;

namespace MiniGame.MovingCubes
{
    [RequireComponent(typeof(Collider))]
    public class FinishZone : MonoBehaviour
    {
        public event Action EndGame;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerCube playerCube))
                EndGame?.Invoke();
        }
    }
}
