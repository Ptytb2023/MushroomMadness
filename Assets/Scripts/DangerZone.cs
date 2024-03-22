using MushroomMadness.Player;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DangerZone : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        Debug.Log(player);
    }


    private void OnValidate()
    {
        GetComponent<Collider>().isTrigger = true;
    }
}
