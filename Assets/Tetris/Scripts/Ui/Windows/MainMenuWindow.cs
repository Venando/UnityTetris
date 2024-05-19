using UnityEngine;
using UnityEngine.UI;

namespace Tetris.Ui.Windows
{
    public class MainMenuWindow : MonoBehaviour
    {
        [SerializeField] private Button _newGameButton;

        private void Awake()
        {
            _newGameButton.onClick.AddListener(OnNewGameButton);
        }

        private void OnNewGameButton()
        {
            SceneController.LoadGame();
        }
    }
}
