using System;
using Tetris.Board;
using UnityEngine;

namespace Tetris.Data
{
    public class RotatableShapeData
    {
        private readonly Vector2Int[][] _rotatedPoints;

        public RotatableShapeData(ShapeData shapeData)
        {
            int rotationsNumber = Enum.GetValues(typeof(ShapeRotation)).Length;
            _rotatedPoints = new Vector2Int[rotationsNumber][];
            _rotatedPoints[(int)ShapeRotation.Up] = shapeData.Offsets;
            _rotatedPoints[(int)ShapeRotation.Right] = CreateRotatedPositions(shapeData.Offsets, 90f);
            _rotatedPoints[(int)ShapeRotation.Down] = CreateRotatedPositions(shapeData.Offsets, 180f);
            _rotatedPoints[(int)ShapeRotation.Left] = CreateRotatedPositions(shapeData.Offsets, 270f);
        }

        private Vector2Int[] CreateRotatedPositions(Vector2Int[] basePositions, float angle)
        {
            var positions = new Vector2Int[basePositions.Length];
            float angleInRad = angle * Mathf.Deg2Rad;
            for (int i = 0; i < positions.Length; i++)
            {
                positions[i] = TurnPositionByAngle(basePositions[i], angleInRad);
            }
            return positions;
        }
        

        private static Vector2Int TurnPositionByAngle(Vector2Int position, float angle)
        {
            float cos = Mathf.Cos(angle);
            float sin = Mathf.Sin(angle);
            int xPos = Mathf.RoundToInt(position.x * cos + position.y * (-sin));
            int yPos = Mathf.RoundToInt(position.x * sin + position.y * cos);
            return new Vector2Int(xPos, yPos);
        }

        public Vector2Int[] GetPositions(ShapeRotation shapeRotation)
        {
            return _rotatedPoints[(int)shapeRotation];
        }
    }
}