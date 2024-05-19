using System;
using Tetris.Data;
using UnityEngine;

namespace Tetris.Board
{
    public class GridSystem
    {
        public delegate void ShapeMoveCallback(Vector2Int from, Vector2Int to, FallingShape fallingShape);
        public delegate void ShapeRotateCallback(ShapeRotation from, ShapeRotation to, FallingShape fallingShape);
        public delegate void ShapeSpawnCallback(Vector2Int position, FallingShape fallingShape);
        public delegate void ShapeRemovedCallback(Vector2Int position, FallingShape fallingShape);
        
        public event ShapeSpawnCallback ShapeSpawn;
        public event ShapeMoveCallback ShapeMove;
        public event ShapeRotateCallback ShapeRotate;
        public event ShapeRemovedCallback ShapeRemoved;
        public event Action GridUpdated;
        public event Action RowCleared;
        
        public int Width { get; }
        public int Height { get; }
        public bool IsStuck { get; private set; }

        private readonly FallingShape _fallingShape = new();
        private readonly GridItem[][] _gridItems;
        private readonly Vector2Int _baseSpawnCoordinate;
        private readonly ShapesProvider _shapesProvider;
        
        public GridSystem(ShapesProvider shapesProvider, int width, int height)
        {
            _shapesProvider = shapesProvider;
            Width = width;
            Height = height;

            _baseSpawnCoordinate = new Vector2Int(Width / 2, height - 1);

            _gridItems = new GridItem[Height][];
            for (var i = 0; i < _gridItems.Length; i++)
            {
                _gridItems[i] = new GridItem[width];
            }
        }

        public void Tick()
        {
            if (TryGetRowToClear(out int rowToClear))
            {
                ClearRow(rowToClear);
            }
            else if (TryGetRowToShrink(out int rowToShrink))
            {
                ShrinkRow(rowToShrink);
            }
            else if (_fallingShape.IsActive)
            {
                MoveFallingShapeDown();
            }
            else
            {
                SpawnShape(_shapesProvider.GetNextShape(), ItemColorHelper.GetRandomItemColor());
            }
        }

        private bool SpawnShape(RotatableShapeData rotatableShapeData, ItemColor itemColor)
        {
            _fallingShape.Initiate(rotatableShapeData, itemColor);
            int spawnOffset = _fallingShape.GetSpawnYOffset();
            Vector2Int spawnCoordinate = _baseSpawnCoordinate - new Vector2Int(0, spawnOffset);

            if (!IsValidFallingShapePosition(spawnCoordinate))
            {
                IsStuck = true;
                return false;
            }
            
            _fallingShape.SetPosition(spawnCoordinate);
            _fallingShape.IsActive = true;
            
            ShapeSpawn?.Invoke(spawnCoordinate, _fallingShape);

            return true;
        }

        public bool MoveFallingShape(Vector2Int offset)
        {
            if (!_fallingShape.IsActive)
            {
                return false;
            }
            
            Vector2Int oldPosition = _fallingShape.GetPosition();
            Vector2Int newPosition = oldPosition + offset;

            if (!IsValidFallingShapePosition(newPosition))
            {
                return false;
            }
            
            _fallingShape.SetPosition(newPosition);
            ShapeMove?.Invoke(oldPosition, newPosition, _fallingShape);
            return true;
        }

        public void TryToRotateFallingShape()
        {
            ShapeRotation oldRotation = _fallingShape.GetRotation();
            
            if (!IsValidFallingShapePosition(_fallingShape.GetPosition()))
            {
                return;
            }

            do
            {
                _fallingShape.Rotate();
            } while (!IsValidFallingShapePosition(_fallingShape.GetPosition()));
            
            ShapeRotate?.Invoke(oldRotation, _fallingShape.GetRotation(), _fallingShape);
        }
        
        private bool IsValidFallingShapePosition(Vector2Int position)
        {
            foreach (Vector2Int cell in _fallingShape.GetCenterOffsets())
            {
                int x = cell.x + position.x;
                int y = cell.y + position.y;
                if (x < 0 || x >= Width || y < 0 || y >= Height || _gridItems[y][x].IsOccupied)
                {
                    return false;
                }
            }
            return true;
        }

        private bool TryGetRowToClear(out int rowToClear)
        {
            rowToClear = -1;
            
            if (!TryGetFirstOccupiedRowIndex(out int occupiedRowIndex))
            {
                return false;
            }

            for (int i = 0; i <= occupiedRowIndex; i++)
            {
                if (IsFullRow(_gridItems[i]))
                {
                    rowToClear = i;
                    return true;
                }
            }
            
            return false;
        }

        private void ClearRow(int rowToClearIndex)
        {
            GridItem[] rowToClear = _gridItems[rowToClearIndex];

            for (var i = 0; i < Width; i++)
            {
                rowToClear[i] = new GridItem();
            }
            
            GridUpdated?.Invoke();
            RowCleared?.Invoke();
        }

        private bool TryGetRowToShrink(out int rowToShrink)
        {
            rowToShrink = -1;
            
            if (!TryGetFirstOccupiedRowIndex(out int occupiedRowIndex))
            {
                return false;
            }

            for (int i = 0; i < occupiedRowIndex; i++)
            {
                if (IsEmptyRow(_gridItems[i]))
                {
                    rowToShrink = i;
                    return true;
                }
            }

            return false;
        }

        private void ShrinkRow(int rowToShrinkIndex)
        {
            GridItem[] rowToShrink = _gridItems[rowToShrinkIndex];

            for (var i = rowToShrinkIndex; i < Height - 1; i++)
            {
                _gridItems[i] = _gridItems[i + 1];
            }

            _gridItems[Height - 1] = rowToShrink;
            
            GridUpdated?.Invoke();
        }

        private bool IsEmptyRow(GridItem[] row)
        {
            for (int j = 0; j < Width; j++)
            {
                if (row[j].IsOccupied)
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsFullRow(GridItem[] row)
        {
            for (int j = 0; j < Width; j++)
            {
                if (!row[j].IsOccupied)
                {
                    return false;
                }
            }
            return true;
        }

        private bool TryGetFirstOccupiedRowIndex(out int rowIndex)
        {
            for (int i = Height - 1; i >= 0; i--)
            {
                GridItem[] row = _gridItems[i];
                for (int j = 0; j < Width; j++)
                {
                    if (row[j].IsOccupied)
                    {
                        rowIndex = i;
                        return true;
                    }
                }
            }

            rowIndex = -1;
            return false;
        }

        private void MoveFallingShapeDown()
        {
            if (MoveFallingShape(Vector2Int.down))
            {
                return;
            }
            
            ShapeRemoved?.Invoke(_fallingShape.GetPosition(), _fallingShape);
            _fallingShape.IsActive = false;
            FixateFallingShape();
        }

        private void FixateFallingShape()
        {
            Vector2Int position = _fallingShape.GetPosition();
            foreach (Vector2Int centerOffset in _fallingShape.GetCenterOffsets())
            {
                Vector2Int occupyPosition = position + centerOffset;

                _gridItems[occupyPosition.y][occupyPosition.x] = new GridItem()
                {
                    ItemColor = _fallingShape.GetItemColor(),
                    IsOccupied = true
                };
            }
            
            GridUpdated?.Invoke();
        }

        public GridItem GetGridItem(int row, int column)
        {
            return _gridItems[row][column];
        }
    }
}
