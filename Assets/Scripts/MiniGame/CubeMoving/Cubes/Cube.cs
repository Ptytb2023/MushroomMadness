using MiniGame.MovingCubes.Config;
using UnityEngine;

namespace MiniGame.MovingCubes.Cubes
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class Cube : MonoBehaviour
    {
        [SerializeField] private DirectionMoving _axisMovements;
        [SerializeField] private FrezePosition _frezePosition;
        [field: SerializeField] protected ConfigCubes Config { get; private set; }

        protected Rigidbody Rigidbody { get; private set; }

        protected virtual void Start()
        {
            Rigidbody = GetComponent<Rigidbody>();
        }

        public abstract void ResetCube();

        public void Move(Vector2 direction)
        {
            switch (_axisMovements)
            {
                case DirectionMoving.All:
                    AddVelocity(new Vector3(direction.x, 0, direction.y));
                    break;
                case DirectionMoving.Horizontal:
                    AddVelocity(new Vector3(direction.x, 0, 0));
                    break;
                case DirectionMoving.Vertical:
                    AddVelocity(new Vector3(0, 0, direction.y));
                    break;
                case DirectionMoving.None:
                    break;

            }
        }

        private void AddVelocity(Vector3 direction)
        {
            Rigidbody.velocity += direction * Config.SeedMove * Time.deltaTime;
        }

        private void FrezeRigidbody()
        {
            if (_frezePosition == FrezePosition.Standart)
            {
                if (_axisMovements == DirectionMoving.Horizontal)
                    Rigidbody.constraints = RigidbodyConstraints.FreezePositionZ;
                else
                    Rigidbody.constraints = RigidbodyConstraints.FreezePositionX;
            }

            Rigidbody.freezeRotation = true;
        }

        private void OnValidate()
        {
            FrezeRigidbody();
        }
    }
}