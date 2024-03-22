using UnityEngine;

namespace MiniGame.MovingCubes.Cubes
{
    public class PlayerCube : BaseCube
    {
        protected override void ResetCube()
        {
            Rigidbody.velocity = Vector3.zero;
            transform.position = StartPosition;
        }
    }
}
