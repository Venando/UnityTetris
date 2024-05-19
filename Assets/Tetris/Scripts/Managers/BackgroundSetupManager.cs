using Tetris.Scriptables;
using UnityEngine;

namespace Tetris.Managers
{
    public class BackgroundSetupManager : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private SpriteRenderer _gridBackground;
        
        public void Setup(GameSettingsScriptableObject gameSettings)
        {
            SetupCamera(gameSettings);
            SetupBackground(gameSettings);
        }

        private void SetupCamera(GameSettingsScriptableObject gameSettings)
        {
            _camera.orthographicSize = (gameSettings.Height / 2f) + 1;
            _camera.transform.position = new Vector3(gameSettings.Width / 2f, gameSettings.Height / 2f, -10f);
        }

        private void SetupBackground(GameSettingsScriptableObject gameSettings)
        {
            _gridBackground.transform.position = new Vector3(gameSettings.Width / 2f, gameSettings.Height / 2f, 0f);
            _gridBackground.size = new Vector2(gameSettings.Width, gameSettings.Height);
        }
    }
}
