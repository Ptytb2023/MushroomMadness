using UnityEngine;

namespace MiniGame.MovingCubes.Config
{
    [CreateAssetMenu(fileName = "Cubes", menuName = "Config/MiniGame/MovingCubes/Cube", order = 51)]
    public class ConfigCubes : ScriptableObject
    {
        [Min(0f)]
        [SerializeField] private float _speedMove;

        [Range(0f, 1f)]
        [SerializeField] private float _alphaTransparency;

        [Min(0f)]
        [SerializeField] private float _durationFade;

        [SerializeField] public float SpeedMove => _speedMove;
        [SerializeField] public float AlphaTransparency => _alphaTransparency;
        [SerializeField] public float DurationFade => _durationFade;
    }
}