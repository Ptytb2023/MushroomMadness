using System;
using UnityEngine;

namespace MiniGame
{
    public interface IHandlerLaunch
    {
        public event Action<bool> EndGame;

        public Transform GetContenerMiniGame();
        public void StartGame(MiniGameManger miniGame);

    }
}