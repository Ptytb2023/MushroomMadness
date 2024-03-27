using MushroomMadness.Config.Player;
using MushroomMadness.InputSystem;
using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace MushroomMadness.Controllers
{
    [RequireComponent(typeof(CharacterController))]
    public class MovePlayerConroller : MonoBehaviour
    {
        [SerializeField] private ConfigMove _congig;
        [SerializeField] private CameraMoving _camera;
        [SerializeField] private AudioSource _audioSource;

        [Inject] private IInputMove _input;

        private CharacterController _characterController;

        private Coroutine _moving;
        private Vector3 _velocity;

        private bool _isGrounded => _characterController.isGrounded;

        public event Action<bool> Jump;
        public event Action<bool> Run;

        private void Start() => _characterController = GetComponent<CharacterController>();

        private void OnEnable()
        {
            _input.ClickMove += OnClickMove;
            _input.ClickJump += OnClickJump;
        }

        private void OnDisable()
        {
            _input.ClickMove -= OnClickMove;
            _input.ClickJump -= OnClickJump;
        }

        private void Update()
        {
            Rotation();
            Move();
        }

        private void FixedUpdate() => Gravitation();


        private void Move() => _characterController.Move(_velocity * Time.deltaTime);

        private void Rotation()
        {
            if (_velocity.x == 0 && _velocity.z == 0)
                return;

            float speed = _congig.RotationSpeed;
            Vector3 moveDircetion = _velocity;
            moveDircetion.y = 0f;

            Quaternion toRotation = Quaternion.LookRotation(moveDircetion);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, speed);
        }

        private void Gravitation()
        {
            if (!_isGrounded)
                _velocity.y += _congig.Gravity * Time.deltaTime;
            else
                Jump?.Invoke(false);
        }

        private void OnClickJump()
        {
            if (_isGrounded)
                StartCoroutine(SetDirectionY());
        }

        private void OnClickMove(bool isClick)
        {
            if (!isClick)
            {
                if (_moving != null)
                    StopCoroutine(_moving);

                _velocity.x = 0f;
                _velocity.z = 0f;

                Run?.Invoke(false);
                _audioSource.Stop();
                return;
            }
            _moving = StartCoroutine(SetDirectionXAndZ());
            _audioSource.Play();
        }


        public IEnumerator SetDirectionXAndZ()
        {
            Run?.Invoke(true);

            float speedMove = _congig.MoveSpeed;

            while (enabled)
            {
                var direction = _input.GetDirectionMove() * speedMove;

                Vector3 move = Quaternion.Euler(0, _camera.transform.eulerAngles.y, 0)
                    * new Vector3(direction.x, 0, direction.y);

                _velocity = new Vector3(move.x, _velocity.y, move.z);

                yield return null;
            }
        }

        public IEnumerator SetDirectionY()
        {
            float elapsedTime = 0f;
            float duration = _congig.DurationJump;
            float height = _congig.HeightJump;
            var animationCurve = _congig.AnimationJump;

            Jump?.Invoke(true);

            while (elapsedTime <= duration && enabled)
            {
                elapsedTime += Time.deltaTime;

                float progres = elapsedTime / duration;
                float directionY = animationCurve.Evaluate(progres) * height;

                _velocity.y = directionY;

                yield return null;
            }


            yield break;
        }


        public void ResetMove()
        {
            StopAllCoroutines();
            Run?.Invoke(false);
            Jump?.Invoke(false);
            _audioSource.Stop();
            _velocity = Vector3.zero;
        }
    }
}
