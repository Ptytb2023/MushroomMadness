using MushroomMadness.InputSystem;
using UnityEngine;
using Zenject;

public class LookingPlayer : MonoBehaviour
{
    [SerializeField] private Transform _pointLook;
    [SerializeField] private float _dumping;
    [SerializeField] private float _viewingTime;
    [SerializeField] private float _speedRotation;
    [SerializeField] private TypeMove _typeMove;
    [SerializeField] private CameraMoving _camera;

    [Inject]
    private IinputInterface _inputInterface;


    private void OnEnable()
    {
        _inputInterface.ClickMap += ChangeLook;
    }

    private void OnDisable()
    {
        _inputInterface.ClickMap -= ChangeLook;
    }

    private void ChangeLook()
    {
        Debug.Log(true);
        _camera.TakeLookTargerByTime(_pointLook, _viewingTime, _dumping, _speedRotation, _typeMove);
    }
}
