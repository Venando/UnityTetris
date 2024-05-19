using UnityEngine;
using UnityEngine.UI;

namespace Tetris.Ui.Windows
{
    public class PausePanelWindow : UiWindowObject
    {
        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private Button _resumeButton;

        protected override void Awake()
        {
            base.Awake();
            _mainMenuButton.onClick.AddListener(OnMainMenuButton);
            _resumeButton.onClick.AddListener(OnResumeButton);
        }

        private void OnEnable()
        {
            PauseController.Pause();
        }

        private void OnMainMenuButton()
        {
            SceneController.LoadMainMenu();
        }

        private void OnResumeButton()
        {
            PauseController.Resume();
            UiManager.ResetToDefault();
        }
    }
}