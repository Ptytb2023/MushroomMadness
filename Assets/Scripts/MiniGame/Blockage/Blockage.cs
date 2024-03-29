using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Blockage : MonoBehaviour
{
    [SerializeField] private float _viewingTimeCamera = 4f;
    [SerializeField] private float _dumpingCamera = 1f;
    [SerializeField] private float _speedRotatin = 2f;

    [Space]
    [SerializeField][Min(0f)] private float _forceExplosion = 400f;
    [SerializeField][Min(0f)] private float _radiusExplosion = 5f;

    [Space]
    [SerializeField] private AudioSource _audioSource;

    private List<Stone> _stones;


    private CameraMoving _camera;
    private Collider _collider;

    private void Start()
    {
        _camera = FindAnyObjectByType<CameraMoving>();
        _collider = GetComponent<Collider>();
        _stones = GetComponentsInChildren<Stone>().ToList();
    }

    public void ExplodePassege()
    {
        StartCoroutine(DelayOpenAndCameraLookMe());
    }

    private IEnumerator DelayOpenAndCameraLookMe()
    {
        float delay = _viewingTimeCamera / 2f;

        _camera.TakeLookTargerByTime(transform, _viewingTimeCamera,
            _dumpingCamera, _speedRotatin, TypeMove.Towards);

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
            if (stone.Rigidbody == null) return;

            stone.Rigidbody.isKinematic = false;
            Explode(stone.Rigidbody);
            stone.Broke();
        }

        _audioSource.Play();
    }

    private void Explode(Rigidbody rigidbody)
    {
        rigidbody.AddExplosionForce(_forceExplosion, transform.position, _radiusExplosion);
    }
}
