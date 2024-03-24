using MushroomMadness.SceneLoadGame;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MushroomMadness.UI.LoadScene
{
    public class ScreenLoadGame : MonoBehaviour
    {
        [SerializeField] private Scrollbar _loadBar;
        [SerializeField] private TMP_Text _label;

        [Space]
        [SerializeField] private SceneLoadManager _sceneLoadManager;

        private const string _loadText = "Загрузка";


        private void OnEnable()
        {
            _sceneLoadManager.ChangeProgress += SetValueProgress;
        }

        private void OnDisable()
        {
            _sceneLoadManager.ChangeProgress -= SetValueProgress;
        }

        private void SetValueProgress(float progress)
        {
            _loadBar.size = progress / 100f;
            _label.text = _loadText + " " + string.Format("{0:0}%", progress);
        }
    }
}
