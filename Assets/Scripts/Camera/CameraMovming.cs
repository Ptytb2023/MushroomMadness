using MushroomMadness.Player;
using System.Collections;
using System.Net.NetworkInformation;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    [SerializeField] private Player _player;

    [Space]
    [Header("Движение камеры")]

    [Min(0)]
    [Tooltip("Скорость камеры")]
    [SerializeField] private float _dumping = 0.22f;

    [Tooltip("Должна ли камера всегда смотреть на персонажа")]
    [SerializeField] private bool _isLookOnTarget = true;


    [Space]
    [Header("Расположение камеры")]

    [Tooltip("Отступ камеры")]
    [SerializeField] private Vector3 _offset;


    private Transform _curentTarget;
    private float _currentDumping;


    private void Start()
    {
        _currentDumping = _dumping;
        _curentTarget = _player.transform;
    }

    private void LateUpdate() => MoveToTarget();

    private void MoveToTarget()
    {
        var newPostion = _curentTarget.position - _offset;
        transform.position = Vector3.Lerp(transform.position, newPostion, _currentDumping * Time.deltaTime);

        if (_isLookOnTarget)
            transform.LookAt(_curentTarget);
    }

    private void FastMoveToTarget()
    {
        var newPostion = _player.transform.position - _offset;
        transform.position = newPostion;

        if (_isLookOnTarget)
            transform.LookAt(_player.transform);
    }

    public void TakeLookByTime(Transform target, float viewingTime,float dumping)
    {
        if (viewingTime < 0) return;

        _curentTarget = target;
        _currentDumping = dumping;

        StartCoroutine(LookNewTarget(viewingTime));
    }

    private IEnumerator LookNewTarget(float viewingTime)
    {
        _player.SetActiveMove(false);


        while (enabled && viewingTime > 0)
        {
            viewingTime -= Time.deltaTime;
            yield return null;
        }

        _curentTarget = _player.transform;
        _currentDumping = _dumping;
        _player.SetActiveMove(true);
    }

    private void OnValidate()
    {
        FastMoveToTarget();
    }
}
