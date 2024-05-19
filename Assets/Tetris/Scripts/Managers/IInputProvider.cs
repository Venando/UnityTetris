using System;

namespace Tetris.Managers
{
    public interface IInputProvider
    {
        bool IsFastMode { get; }
        
        event Action RotateShape;
        event Action MoveLeft;
        event Action MoveRight;
        event Action<bool> FastModeSwitch;
    }
}