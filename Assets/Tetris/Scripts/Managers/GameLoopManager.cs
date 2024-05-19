using System.Collections;
using Tetris.Board;
using Tetris.Scriptables;
using Tetris.Ui;
using Tetris.Ui.Windows;
using UnityEngine;

namespace Tetris.Managers
{
    public class GameLoopManager : MonoBehaviour
    {
        private GameSettingsScriptableObject _gameSettings;
        private IInputProvider _inputProvider;
        private GridSystem _gridSystem;
        private ScoreManager _scoreManager;
        private GameUiManager _gameUiManager;
        
        public void Initiate(GridSystem gridSystem, ScoreManager scoreManager, GameUiManager gameUiManager, GameSettingsScriptableObject gameSettings,
            IInputProvider inputProvider)
        {
            _gridSystem = gridSystem;
            _scoreManager = scoreManager;
            _gameUiManager = gameUiManager;
            _inputProvider = inputProvider;
            _gameSettings = gameSettings;
            StartCoroutine(GameLoop());
        }
        
        private IEnumerator GameLoop()
        {
            var waitForSeconds = new WaitForSeconds(_gameSettings.TickTime);
            var fastWaitForSeconds = new WaitForSeconds(_gameSettings.FastModeTickTime);

            while (true)
            {
                yield return _inputProvider.IsFastMode ? fastWaitForSeconds : waitForSeconds;
                
                _gridSystem.Tick();
                
                if (_gridSystem.IsStuck)
                {
                    break;
                }
            }

            var gameOverWindow = _gameUiManager.Open<GameOverWindow>();
            gameOverWindow.Initiate(_scoreManager.Score);
        }
    }
}