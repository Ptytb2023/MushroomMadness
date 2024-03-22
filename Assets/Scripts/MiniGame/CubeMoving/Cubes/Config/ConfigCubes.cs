using UnityEngine;

namespace MiniGame.MovingCubes.Config
{
    [CreateAssetMenu(fileName = "Cubes", menuName = "Config/MiniGame/MovingCubes/Cube", order = 51)]
    public class ConfigCubes : ScriptableObject
    {
        [field: SerializeField] public float SeedMove { get; private set; }
        [field: SerializeField] public float AlphaTransparency { get; private set; }
        [field: SerializeField] public float DurationFade { get; private set; }
    }
}