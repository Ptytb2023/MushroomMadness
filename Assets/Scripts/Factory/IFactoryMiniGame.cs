using UnityEngine;

namespace MiniGame.Factory
{
    public interface IFactoryMiniGame
    {
        public MiniGameManger GetNewInstansMiniGame(MiniGameManger prefab,Transform parent);
    }
}