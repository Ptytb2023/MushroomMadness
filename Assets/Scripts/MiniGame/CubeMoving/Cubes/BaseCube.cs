using MiniGame.MovingCubes.Config;
using UnityEngine;

namespace MiniGame.MovingCubes.Cubes
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class BaseCube : MonoBehaviour, IResetGame
    {
        [SerializeField] private FrezePosition _frezePosition;

        [field: SerializeField] protected DirectionMoving AxisMovement { get; private set; }
        protected ConfigCubes Config { get; private set; }


        protected Vector3 StartPosition;
        protected Rigidbody Rigidbody { get; private set; }

        protected DirectionMoving CurrentAxisMovemnet;


        protected virtual void Start()
        {
            Rigidbody = GetComponent<Rigidbody>();
            StartPosition = transform.position;
            CurrentAxisMovemnet = AxisMovement;

            FrezeRigidbody();
        }

        public virtual void Init(ConfigCubes config)
        {
            if (Config != null)
                return;

            Config = config;
        }

        protected abstract void ResetCube();

        public void Move(Vector2 direction)
        {
            switch (CurrentAxisMovemnet)
            {
                case DirectionMoving.None: break;

                case DirectionMoving.Horizontal:
                    AddVelocity(new Vector3(direction.x, 0, 0));
                    break;

                case DirectionMoving.Vertical:
                    AddVelocity(new Vector3(0, 0, direction.y));
                    break;

                case DirectionMoving.All:
                    AddVelocity(new Vector3(direction.x, 0, direction.y));
                    break;
            }
        }

        private void AddVelocity(Vector3 direction)
        {
            Rigidbody.velocity += direction * Config.SpeedMove * Time.deltaTime;
        }

        private void FrezeRigidbody()
        {
            if (_frezePosition == FrezePosition.Standart)
            {
                if (AxisMovement == DirectionMoving.Horizontal)
                    Rigidbody.constraints = RigidbodyConstraints.FreezePositionZ;
                else if (AxisMovement == DirectionMoving.Vertical)
                    Rigidbody.constraints = RigidbodyConstraints.FreezePositionX;
            }

            Rigidbody.freezeRotation = true;
        }

        public void Resetting()
        {
            ResetCube();
        }
    }
}