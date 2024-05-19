using Tetris.Board;
using Tetris.GridDisplay;
using Tetris.Scriptables;
using Tetris.Ui;
using UnityEngine;

namespace Tetris.Managers
{
    public class GameSetupManager : MonoBehaviour
    {
        [SerializeField] private GameGrid _gameGrid;
        [SerializeField] private NextShapePreviewGrid _nextShapePreviewGrid;
        [SerializeField] private InputProvider _inputProvider;
        [SerializeField] private BackgroundSetupManager _backgroundSetupManager;
        [SerializeField] private GameLoopManager _gameLoopManager;
        [SerializeField] private ShapesScriptableObject _shapesScriptableObject;
        [SerializeField] private GameSettingsScriptableObject _gameSettings;
        [SerializeField] private TilesScriptableObject _tilesScriptableObject;
        [SerializeField] private GameUiManager _gameUiManager;
        
        private GridSystem _gridSystem;
        private PlayerController _playerController;
        private ScoreManager _scoreManager;
        private ShapesProvider _shapesProvider;
        
        private void Awake()
        {
            _shapesProvider = new ShapesProvider(_shapesScriptableObject);
            _gridSystem = new GridSystem(_shapesProvider, _gameSettings.Width, _gameSettings.Height);
            _playerController = new PlayerController(_inputProvider, _gridSystem);
            _scoreManager = new ScoreManager(_gridSystem, _gameSettings);
            _backgroundSetupManager.Setup(_gameSettings);
        }

        private void Start()
        {
            InitiateGame();
        }

        private void InitiateGame()
        {
            _nextShapePreviewGrid.Initiate(_shapesProvider, _tilesScriptableObject);
            _gameGrid.Initiate(_tilesScriptableObject, _gridSystem);
            _gameLoopManager.Initiate(_gridSystem, _scoreManager, _gameUiManager, _gameSettings, _inputProvider);
        }

        private void OnDestroy()
        {
            _playerController?.Dispose();
            _scoreManager?.Dispose();
        }
    }
}
