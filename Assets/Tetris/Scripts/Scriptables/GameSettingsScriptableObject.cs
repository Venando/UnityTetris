using UnityEngine;

namespace Tetris.Scriptables
{
    [CreateAssetMenu]
    public class GameSettingsScriptableObject : ScriptableObject
    {
        public int Width => _width;
        public int Height => _height;
        public float FastModeTickTime => _fastModeTickTime;
        public float TickTime => _tickTime;
        public int ScorePerClearedRow => _scorePerClearedRow;

        [SerializeField] private int _width = 10;
        [SerializeField] private int _height = 20;
        [SerializeField] private float _fastModeTickTime = 0.075f;
        [SerializeField] private float _tickTime = 0.4f;
        [SerializeField] private int _scorePerClearedRow = 50;
    }
}