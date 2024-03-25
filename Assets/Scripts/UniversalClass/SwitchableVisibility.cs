using DG.Tweening;
using UnityEngine;

public class SwitchableVisibility
{
    private MeshRenderer _meshRendere;

    private float _duration;
    private float _alphaOff;
    private const float _alphaOn = 1f;

    public SwitchableVisibility(MeshRenderer meshRendere, float alphaTransparency, float durationFade)
    {
        _meshRendere = meshRendere;
        _alphaOff = alphaTransparency;
        _duration = durationFade;
    }


    public void SetActive(bool enabel)
    {
        if (enabel)
            SetAlpha(_alphaOn);
        else if (!enabel)
            SetAlpha(_alphaOff);
    }

    private void SetAlpha(float alpha)
    {
        foreach (var material in _meshRendere.materials)
        {
            material.DOFade(alpha, _duration);
        }
    }

}
