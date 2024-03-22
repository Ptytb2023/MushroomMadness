using UnityEngine;

namespace MiniGame.MovingCubes.Cubes
{
    [RequireComponent(typeof(MeshRenderer), typeof(Collider))]
    public class SwitchableCube : Cube
    {
        [SerializeField] private StateVisible _startVisible;

        private SwitchableVisibility _switchableVisibility;
        private Vector3 _startPosition;
        private StateVisible _curentVisible;

        protected override void Start()
        {
            base.Start();

            var meshReder = GetComponent<MeshRenderer>();
            var colider = GetComponent<Collider>();

            _curentVisible = _startVisible;
            _startPosition = transform.position;
            _switchableVisibility = new SwitchableVisibility(meshReder, colider, Config.AlphaTransparency, Config.DurationFade);
        }

        public override void ResetCube()
        {
            Rigidbody.velocity = Vector3.zero;
            transform.position = _startPosition;
            _curentVisible = _startVisible;
            Toggle();
        }

        public void Toggle()
        {
            if (_curentVisible == StateVisible.Off)
                _switchableVisibility.SetActive(false);
            else if (_curentVisible == StateVisible.On)
                _switchableVisibility.SetActive(true);
        }
    }
}