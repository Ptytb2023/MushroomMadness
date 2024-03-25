using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MushroomMadness.SceneLoadGame
{
    public class SceneLoadManager : MonoBehaviour
    {
        private const float _delayLoad = 1f;
        private AsyncOperation _asyncOperation;

        public event Action<float> ChangeProgress;
        public event Action ReadySceneLoad;


        private Coroutine _loadScene;
        public void LoadScene(int SceneID)
        {
            if (_loadScene != null) return;
            _loadScene = StartCoroutine(LoadSceneCor(SceneID));
        }

        private IEnumerator LoadSceneCor(int SceneId)
        {
            yield return new WaitForSecondsRealtime(_delayLoad);
            _asyncOperation = SceneManager.LoadSceneAsync(SceneId);

            while (!_asyncOperation.isDone)
            {
                float progress = _asyncOperation.progress;
                ChangeProgress?.Invoke(progress);

                yield return null;
            }

            _loadScene = null;

            ReadySceneLoad?.Invoke();
        }
    }
}
