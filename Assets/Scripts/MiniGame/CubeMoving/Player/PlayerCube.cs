using UnityEngine;

namespace MiniGame.MovingCubes.Cubes
{
    public class PlayerCube : BaseCube
    {
        protected override void ResetCube()
        {
            RigidbodyCube.velocity = Vector3.zero;
            transform.position = StartPosition;
        }
    }
}
