using UnityEngine;
using Zenject;

namespace MiniGame.Factory
{
    public class FactoryMiniGame : MonoBehaviour
    {
        [Inject]
        private DiContainer _diContaner;

        public MiniGameManger GetNewInstansMiniGame(MiniGameManger prefab, Transform parent)
        {
            return _diContaner.InstantiatePrefab(prefab, parent).GetComponent<MiniGameManger>();
        }
    }
}
