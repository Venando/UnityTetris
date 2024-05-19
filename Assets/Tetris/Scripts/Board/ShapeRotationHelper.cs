using System;

namespace Tetris.Board
{
    public static class ShapeRotationHelper
    {
        public static ShapeRotation GetNextRotation(this ShapeRotation shapeRotation)
        {
            return shapeRotation switch
            {
                ShapeRotation.Up => ShapeRotation.Right,
                ShapeRotation.Right => ShapeRotation.Down,
                ShapeRotation.Down => ShapeRotation.Left,
                ShapeRotation.Left => ShapeRotation.Up,
                _ => throw new ArgumentOutOfRangeException(nameof(shapeRotation), shapeRotation, null)
            };
        }
    }
}