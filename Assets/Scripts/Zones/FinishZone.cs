using System;
using UnityEngine;

namespace MushroomMadness.Zone
{
    [RequireComponent(typeof(Collider))]
    public class FinishZone : MonoBehaviour
    {
        public event Action PlayerReachedFinish;

        private void Start()
        {
            GetComponent<Collider>().isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player.Player player))
                PlayerReachedFinish?.Invoke();
        }
    }
}
