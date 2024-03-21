using System.Collections;
using TMPro;
using UnityEngine;

public class BlinkText : MonoBehaviour
{
    [SerializeField] private TMP_Text _label;
    [SerializeField] private float _speedBlink;

    private string _text;
    private float _timeLife;

    public void Init(string text, float timeLife = 0)
    {
        _text = text;
        _timeLife = timeLife;
        _label.text = text;
    }

    private void OnEnable()
    {
        _label.text = _text;
    }

    private IEnumerator Blinking()
    {
       yield return null;

    }
}
