using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MushroomMadness.UI.LoadScene
{
    public class ScreenLoadGame : MonoBehaviour
    {
        [SerializeField] private Scrollbar _loadBar;
        [SerializeField] private TMP_Text _label;
        [SerializeField][Min(0)] private float _delayLoad;

        private AsyncOperation _asyncOperation;

        private const string _loadText = "Загрузка";

        public void LoadScene(int SceneId)
        {
            if (!enabled) throw new NullReferenceException();

            StartCoroutine(LoadSceneCor(SceneId));
        }

        private IEnumerator LoadSceneCor(int SceneId)
        {
            yield return new WaitForSeconds(_delayLoad);
            _asyncOperation = SceneManager.LoadSceneAsync(SceneId);

            while (!_asyncOperation.isDone)
            {
                float progress = _asyncOperation.progress;
                SetValueProgress(progress);
                yield return null;
            }
        }


        private void SetValueProgress(float progress)
        {
            _loadBar.size = progress / 100f;
            _label.text = _loadText + " " + string.Format("{0:0}%", progress);
        }
    }
}
