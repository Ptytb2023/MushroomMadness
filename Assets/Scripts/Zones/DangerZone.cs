using UnityEngine;

namespace MushroomMadness.Zone
{
    [RequireComponent(typeof(Collider))]
    public class DangerZone : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<Collider>().isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Player.Player>(out Player.Player player))
                player.ResetPlayer();

        }
    }
}