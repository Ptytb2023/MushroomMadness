using UnityEngine;

namespace MiniGame.CubesMoving.Cube
{
    [RequireComponent(typeof(Rigidbody))]
    public class CubeMoving : MonoBehaviour
    {
        [SerializeField] private EDirectionMoving _axisMovements;
        [SerializeField] private bool _isMoving = true;
        [SerializeField] private bool _notBlockAxis = false;


        private Rigidbody _rigidbody;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            FreezeAxis();
            FreezeRotation();
        }

        public void Move(Vector2 direction, float speed)
        {
            if (_isMoving)
                if (_axisMovements == EDirectionMoving.Horizontal)
                    _rigidbody.velocity += new Vector3(direction.x, 0, 0) * speed * Time.deltaTime;
                else
                    _rigidbody.velocity += new Vector3(0, 0, direction.y) * speed * Time.deltaTime;
        }

        public void StopMove()
        {
            _rigidbody.velocity = Vector2.zero;
        }

        protected virtual void FreezeAxis()
        {
            if (_notBlockAxis)
                return;

            if (_axisMovements == EDirectionMoving.Horizontal)
                _rigidbody.constraints = RigidbodyConstraints.FreezePositionZ;
            else
                _rigidbody.constraints = RigidbodyConstraints.FreezePositionX;
        }

        protected virtual void FreezeRotation()
        {
            _rigidbody.freezeRotation = true;
        }
    }
}