using MushroomMadness.InputSystem;
using MushroomMadness.UI.LoadScene;
using MushroomMadness.UI.Pause;
using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ScreenPause _screenPause;
    [SerializeField] private ScreenLoadGame _loadGame;


    [Inject]
    private IInputMiniGame _inputMiniGame;


    private void OnEnable()
    {
       // _inputMiniGame.ClickExitGame
    }


}
