using System;
using Tetris.Board;
using Tetris.Messages;
using Tetris.Scriptables;

namespace Tetris.Managers
{
    public class ScoreManager : IDisposable
    {
        public int Score => _score;
        
        private readonly GridSystem _gridSystem;
        private readonly GameSettingsScriptableObject _gameSettings;

        private int _score;
        
        public ScoreManager(GridSystem gridSystem, GameSettingsScriptableObject gameSettings)
        {
            _gridSystem = gridSystem;
            _gridSystem.RowCleared += OnGridCleared;
            _gameSettings = gameSettings;
        }

        private void OnGridCleared()
        {
            _score += _gameSettings.ScorePerClearedRow;
            Messenger.Publish(new ScoreUpdateMessage(_score));
        }

        public void Dispose()
        {
            _gridSystem.RowCleared -= OnGridCleared;
        }
    }
}