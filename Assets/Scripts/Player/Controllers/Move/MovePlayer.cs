using MushroomMadness.Config.Player;
using System.Collections;
using UnityEngine;

namespace MushroomMadness.Controllers
{
    [RequireComponent(typeof(CharacterController))]
    public class MovePlayer : MonoBehaviour
    {
        [SerializeField] private ConfigMove _congig;

        private CharacterController _characterController;

        private Coroutine _moving;
        private Vector3 _velocity;

        private bool _isGrounded => _characterController.isGrounded;

        private void Start() => _characterController = GetComponent<CharacterController>();

        private void OnEnable()
        {
            _congig.Input.ClickMove += OnClickMove;
            _congig.Input.ClickJump += OnClickJump;

        }

        private void OnDisable()
        {
            _congig.Input.ClickMove -= OnClickMove;
            _congig.Input.ClickJump -= OnClickJump;
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

            float speed = _congig.RotationSpeed * Time.deltaTime;

            Vector3 moveDircetion = _velocity;
            moveDircetion.y = 0f;
            Quaternion toRotation = Quaternion.LookRotation(moveDircetion);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, speed);
        }
        private void Gravitation()
        {
            if (!_isGrounded)
                _velocity.y += _congig.Gravity * Time.deltaTime;
        }


        private void OnClickJump()
        {
            if (_isGrounded)
                StartCoroutine(SetDirectionY());
        }

        private void OnClickMove(bool isClick)
        {
            if (!isClick) return;

            if (_moving != null)
                StopCoroutine(_moving);

            _moving = StartCoroutine(SetDirectionXAndZ());
        }


        public IEnumerator SetDirectionXAndZ()
        {
            float speedMove = _congig.MoveSpeed;

            while (enabled)
            {
                Vector2 direction = _congig.Input.GetDirectionMove() * speedMove;
                _velocity = new Vector3(direction.x, _velocity.y, direction.y);

                yield return null;
            }
        }

        public IEnumerator SetDirectionY()
        {
            float elapsedTime = 0f;
            float duration = _congig.DurationJump;
            float height = _congig.HeightJump;
            var animationCurve = _congig.AnimationJump;

            while (elapsedTime <= duration)
            {
                elapsedTime += Time.deltaTime;

                float progres = elapsedTime / duration;
                float directionY = animationCurve.Evaluate(progres) * height;

                _velocity.y = directionY;

                yield return null;
            }

            yield break;
        }
    }
}
