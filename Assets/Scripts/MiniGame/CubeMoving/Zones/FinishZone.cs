using System;
using UnityEngine;

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
