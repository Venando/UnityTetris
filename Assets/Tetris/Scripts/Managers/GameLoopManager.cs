using System;
using System.Collections;
using Tetris.Board;
using Tetris.Messages;
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

        private WaitForSeconds _tickWaiter;
        private WaitForSeconds _fastTickWaiter;
        
        public void Initiate(GridSystem gridSystem, ScoreManager scoreManager, GameUiManager gameUiManager, GameSettingsScriptableObject gameSettings,
            IInputProvider inputProvider)
        {
            _gridSystem = gridSystem;
            _scoreManager = scoreManager;
            _gameUiManager = gameUiManager;
            _inputProvider = inputProvider;
            _gameSettings = gameSettings;
            UpdateGameSpeed(0);
            StartCoroutine(GameLoop());
            StartCoroutine(GameDifficultyIncrease());
        }
        
        private IEnumerator GameLoop()
        {
            while (true)
            {
                yield return _inputProvider.IsFastMode ? _fastTickWaiter : _tickWaiter;
                _gridSystem.Tick();
                if (_gridSystem.IsStuck)
                {
                    break;
                }
            }

            var gameOverWindow = _gameUiManager.Open<GameOverWindow>();
            gameOverWindow.Initiate(_scoreManager.Score);
        }

        private IEnumerator GameDifficultyIncrease()
        {
            var waiter = new WaitForSeconds(_gameSettings.SpeedIncreaseEvery);
            int difficulty = 0;
            
            while (true)
            {
                yield return waiter;

                difficulty++;

                UpdateGameSpeed(difficulty);
                
                if (_gridSystem.IsStuck)
                {
                    break;
                }
            }
        }

        private void UpdateGameSpeed(int difficulty)
        {
            float speedFactor = 1f + difficulty * _gameSettings.SpeedIncreaseBy;
            Messenger.Publish(new GameSpeedUpdatedMessage(speedFactor));
            _tickWaiter = new WaitForSeconds(_gameSettings.TickTime / speedFactor);
            _fastTickWaiter = new WaitForSeconds(_gameSettings.FastModeTickTime / speedFactor);
        }
    }
}