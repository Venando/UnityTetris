using UnityEngine;

namespace Tetris
{
    public static class PauseController
    {
        private static bool _isPause;
        
        public static bool SwitchPause()
        {
            _isPause = !_isPause;
            Time.timeScale = _isPause ? 0 : 1;
            return _isPause;
        }

        public static void Resume()
        {
            Time.timeScale = 1;
            _isPause = false;
        }

        public static void Pause()
        {
            Time.timeScale = 0;
            _isPause = true;
        }
        
        public static bool IsPause()
        {
            return _isPause;
        }
    }
}