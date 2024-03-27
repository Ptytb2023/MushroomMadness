using MushroomMadness.Player;
using System.Collections;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    [SerializeField] private Player _player;

    [Space]
    [Header("Движение камеры")]

    [Min(0)]
    [Tooltip("Скорость камеры")]
    [SerializeField] private float _dumping = 0.22f;
    [SerializeField] private float _speedRotatin = 0f;

    [Tooltip("Должна ли камера всегда смотреть на персонажа")]
    [SerializeField] private bool _isLookOnTarget = true;


    [Space]
    [Header("Расположение камеры")]

    [Tooltip("Отступ камеры")]
    [SerializeField] private Vector3 _offset;


    private Transform _curentTarget;
    private float _currentDumping;
    private float _currentSpeedRotatin;


    private readonly TypeMove _startTypeMove = TypeMove.Lerp;
    private TypeMove _currentTypeMove;


    private void Start() => SetDefaultParametrs();

    private void LateUpdate() => MoveToTarget();

    private void MoveToTarget()
    {
        var direction = _curentTarget.position - _offset;
        transform.position = GetNewPositin(transform.position, direction, _currentTypeMove);

        if (_isLookOnTarget)
            LookAtTarger();
    }

    private void LookAtTarger()
    {
        var targetRotation = Quaternion.LookRotation(_curentTarget.transform.position - transform.position);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 
            _currentSpeedRotatin * Time.deltaTime);
    }

    private Vector3 GetNewPositin(Vector3 position, Vector3 direction, TypeMove type)
    {
        switch (type)
        {
            case TypeMove.Lerp:

                return Vector3.Lerp(position, direction, _currentDumping * Time.deltaTime);

            case TypeMove.Towards:
                return Vector3.MoveTowards(position, direction, _currentDumping * Time.deltaTime);

            case TypeMove.Slerp:
                return Vector3.Slerp(position, direction, _currentDumping * Time.deltaTime);

            default:
                return Vector3.zero;
        }
    }


    public void TakeLookByTime(Transform target, float viewingTime,
        float dumping, float speedRotation, TypeMove typeMove)
    {
        if (viewingTime < 0) return;

        _currentSpeedRotatin = speedRotation;
        _currentTypeMove = typeMove;
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

        SetDefaultParametrs();
        _player.SetActiveMove(true);
    }

    private void SetDefaultParametrs()
    {
        _curentTarget = _player.transform;
        _currentDumping = _dumping;
        _currentTypeMove = _startTypeMove;
        _currentSpeedRotatin = _speedRotatin;
    }

    private void OnValidate()
    {
        var newPostion = _player.transform.position - _offset;
        transform.position = newPostion;

        if (_isLookOnTarget)
            transform.LookAt(_player.transform);
    }
}

public enum TypeMove
{
    Lerp = 1,
    Towards = 2,
    Slerp = 3,
}
