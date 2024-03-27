using UnityEngine;

namespace MushroomMadness.Audio
{
    public class AudioManader : MonoBehaviour
    {
        [SerializeField] private AudioSource _backgroudn;
        [SerializeField] private AudioSource[] _effects;

        public void SetVolumeBackground(float volume)
        {
            _backgroudn.volume = volume;
        }

        public void SetVolumeEffects(float volume)
        {
            foreach (var effect in _effects)
            {
                effect.volume = volume;
            }
        }

    }
}