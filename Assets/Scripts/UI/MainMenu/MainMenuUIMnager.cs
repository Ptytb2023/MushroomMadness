using MushroomMadness.SceneLoadGame;
using MushroomMadness.UI.Buttons;
using MushroomMadness.UI.LoadScene;
using UnityEngine;

namespace MushroomMadness.UI.MainMenu
{
    public class MainMenuUIMnager : MonoBehaviour
    {
        [SerializeField] private ScreenAboutAuthors _aboutAuthors;
        [SerializeField] private ScreenLoadGame _load;

        [Space]
        [SerializeField] private ButtonStartGame _startGameButton;
        [SerializeField] private ButtonExitGame _exitGameButton;
        [SerializeField] private ButtonAboutAuthors _aboutAuthorsButton;
  

        [Space]
        [SerializeField] private SceneLoadManager _sceneLoadManager;
        [SerializeField] private int _sceneIDGame;

        private void OnEnable()
        {
            _startGameButton.OnButtonClick += StartGame;
            _exitGameButton.OnButtonClick += ExitGame;
            _aboutAuthorsButton.OnButtonClick += OpenScrinAuthorsButton;
        }

        private void OnDisable()
        {
            _startGameButton.OnButtonClick -= StartGame;
            _exitGameButton.OnButtonClick -= ExitGame;
            _aboutAuthorsButton.OnButtonClick -= OpenScrinAuthorsButton;
        }

        private void StartGame()
        {
            _load.gameObject.SetActive(true);
            _sceneLoadManager.LoadScene(_sceneIDGame);
        }

        private void ExitGame()
        {
            Application.Quit();
        }

        private void OpenScrinAuthorsButton()
        {
            _aboutAuthors.ClosePanel += CloseScreenAuthorsButton;
            SetActiveScreenAndButtonTurnOff(true);
        }

        private void CloseScreenAuthorsButton()
        {
            _aboutAuthors.ClosePanel -= CloseScreenAuthorsButton;
            SetActiveScreenAndButtonTurnOff(false);

        }

        private void SetActiveScreenAndButtonTurnOff(bool active)
        {
            _aboutAuthors.gameObject.SetActive(active);

            _startGameButton.gameObject.SetActive(!active);
            _exitGameButton.gameObject.SetActive(!active);
            _aboutAuthorsButton.gameObject.SetActive(!active);
        }
    }
}