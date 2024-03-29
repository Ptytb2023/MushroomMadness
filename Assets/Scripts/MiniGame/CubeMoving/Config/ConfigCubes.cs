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

        [Space]
        [SerializeField] private Sprite _spriteMoveHorizontal;
        [SerializeField] private Sprite _spriteMoveVertical;
        [SerializeField] private Sprite _spriteMoveAll;
        [SerializeField] private Sprite _spriteMoveNone;


        public float SpeedMove => _speedMove;
        public float AlphaTransparency => _alphaTransparency;
        public float DurationFade => _durationFade;

        public Sprite SpriteMoveVertical => _spriteMoveVertical;
        public Sprite SpriteMoveHoriizontal => _spriteMoveHorizontal;
        public Sprite SpriteMoveAll => _spriteMoveAll;
        public Sprite SpriteMoveNone => _spriteMoveNone;
    }
}