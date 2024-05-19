using System.Linq;
using Tetris.Data;
using UnityEngine;

namespace Tetris.Board
{
    public class FallingShape
    {
        public bool IsActive { get; set; }
        
        private RotatableShapeData _rotatableShapeData;
        private ItemColor _itemColor;
        private ShapeRotation _shapeRotation;
        private Vector2Int _position;

        public void Initiate(RotatableShapeData rotatableShapeData, ItemColor itemColor)
        {
            _rotatableShapeData = rotatableShapeData;
            _itemColor = itemColor;
            _shapeRotation = ShapeRotation.Up;
        }

        public void SetPosition(Vector2Int position)
        {
            _position = position;
        }

        public void Rotate()
        {
            _shapeRotation = _shapeRotation.GetNextRotation();
        }
        
        public Vector2Int[] GetCenterOffsets()
        {
            return GetCenterOffsets(_shapeRotation);
        }
        
        public Vector2Int[] GetCenterOffsets(ShapeRotation shapeRotation)
        {
            return _rotatableShapeData.GetPositions(shapeRotation);
        }

        public int GetSpawnYOffset()
        {
            return GetCenterOffsets().Max(pos => pos.y);
        }

        public Vector2Int GetPosition()
        {
            return _position;
        }

        public ItemColor GetItemColor()
        {
            return _itemColor;
        }

        public ShapeRotation GetRotation()
        {
            return _shapeRotation;
        }
    }
}