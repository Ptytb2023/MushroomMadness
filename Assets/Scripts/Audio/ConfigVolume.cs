using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Volume", menuName = "Config/ConfigAudio/Volume", order = 51)]
public class ConfigVolume : ScriptableObject
{
    [Range(0f, 1f)] private float _volume;

    public event Action<float> VolumeChanged;

    public void SetValue(float value)
    {
        if (value > 1 || value < 0) return;
        _volume = value;
        VolumeChanged?.Invoke(_volume);
    }
}
