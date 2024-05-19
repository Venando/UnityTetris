using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Tetris.Ui.Windows
{
    public class GameOverWindow : UiWindowObject
    {
        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private TMP_Text _scoreText;

        protected override void Awake()
        {
            base.Awake();
            _mainMenuButton.onClick.AddListener(OnMainMenuButton);
            _restartButton.onClick.AddListener(OnRestartButton);
        }

        public void Initiate(int scoreManagerScore)
        {
            _scoreText.text = $"Score: {scoreManagerScore.ToString()}";
        }

        private void OnMainMenuButton()
        {
            SceneController.LoadMainMenu();
        }

        private void OnRestartButton()
        {
            SceneController.LoadGame();
        }
    }
}