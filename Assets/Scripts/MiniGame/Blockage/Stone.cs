using DG.Tweening;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(MeshRenderer))]
public class Stone : MonoBehaviour
{
    [SerializeField][Min(0f)] private float _delayDestroy;
    [SerializeField] private float _timeFade;
    [SerializeField] private float _colorChangeSpeed;

    private Rigidbody _rigidbody;
    private MeshRenderer _meshRenderer;

    private const float _alphaVisible = 1f;
    private const float _alphaNotVisible = 0.3f;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    [ContextMenu("Test")]
    public void Broke()
    {
        StartCoroutine(DeleyDestroy());
    }

    private IEnumerator DeleyDestroy()
    {
        while (enabled && _delayDestroy > 0)
        {
            _delayDestroy -= Time.deltaTime;
            yield return null;
        }

        yield return StartCoroutine(BlinkStone());

        Destroy(gameObject);
    }
    private IEnumerator BlinkStone()
    {
        while (enabled && _timeFade > 0)
        {
            _timeFade -= Time.deltaTime;

            _meshRenderer.material.DOFade(_alphaVisible, _colorChangeSpeed);
            yield return new WaitForSeconds(_colorChangeSpeed);

            _meshRenderer.material.DOFade(_alphaNotVisible, _colorChangeSpeed);
            yield return new WaitForSeconds(_colorChangeSpeed);

            _timeFade -= _colorChangeSpeed + _colorChangeSpeed;
        }
    }

    private void OnValidate()
    {
        GetComponent<Rigidbody>().isKinematic = true;
    }
}
