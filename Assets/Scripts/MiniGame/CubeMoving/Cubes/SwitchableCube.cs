using MiniGame.MovingCubes.Config;
using UnityEngine;

namespace MiniGame.MovingCubes.Cubes
{
    [RequireComponent(typeof(MeshRenderer), typeof(Collider))]
    public class SwitchableCube : BaseCube, IVisible
    {
        [SerializeField] private StateVisible _startVisible = StateVisible.On;

        private Collider _collider;
        private SwitchableVisibility _switchableVisibility;
        private StateVisible _curentVisible;

        public void SetVisible(StateVisible state)
        {
            _curentVisible = state;
            ApplyVisible();
        }

        public override void Init(ConfigCubes config)
        {
            base.Init(config);

            var meshReder = GetComponent<MeshRenderer>();
            _collider = GetComponent<Collider>();

            _curentVisible = _startVisible;

            _switchableVisibility = new SwitchableVisibility(meshReder, Config.AlphaTransparency, Config.DurationFade);

            ApplyVisible();
        }

        protected override void ResetCube()
        {
            RigidbodyCube.velocity = Vector3.zero;
            transform.position = StartPosition;
            _curentVisible = _startVisible;

            ApplyVisible();
        }

        public void ApplyVisible()
        {
            if (_curentVisible == StateVisible.Off)
            {
                _switchableVisibility.SetActive(false);
                CurrentAxisMovemnet = DirectionMoving.None;

                _collider.enabled = false;
                RigidbodyCube.isKinematic = true;
            }

            else if (_curentVisible == StateVisible.On)
            {
                _switchableVisibility.SetActive(true);
                CurrentAxisMovemnet = AxisMovement;

                _collider.enabled = true;
                RigidbodyCube.isKinematic = false;
            }
        }
        
    }
}