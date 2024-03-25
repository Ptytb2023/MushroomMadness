using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blockage : MonoBehaviour
{
    [SerializeField] private float _viewingTimeCamera = 4f;

    [Space]
    [SerializeField][Min(0f)] private float _forceExplosion = 400f;
    [SerializeField][Min(0f)] private float _radiusExplosion = 5f;

    [Space]
    [SerializeField] private List<Stone> _stones;


    private CameraMoving _camera;
    private Collider _collider;

    private void Start()
    {
        _camera = FindAnyObjectByType<CameraMoving>();
        _collider = GetComponent<Collider>();
    }

    public void ExplodePassege()
    {
        StartCoroutine(DelayOpenAndCameraLookMe());
    }

    private IEnumerator DelayOpenAndCameraLookMe()
    {
        float delay = _viewingTimeCamera / 2f;
        _camera.TakeLookByTime(transform, _viewingTimeCamera, delay);

        while (enabled && delay > 0)
        {
            delay -= Time.deltaTime;
            yield return null;
        }

        StoneBroke();
    }

    private void StoneBroke()
    {
        _collider.enabled = false;

        foreach (var stone in _stones)
        {
            stone.Broke();
            Explode(stone.Rigidbody);
        }

    }

    private void Explode(Rigidbody rigidbody)
    {
        rigidbody.AddExplosionForce(_forceExplosion, transform.position, _radiusExplosion);
    }
}
