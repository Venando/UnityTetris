using Tetris.Board;
using Tetris.Scriptables;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tetris.GridDisplay
{
    public class GameGrid : MonoBehaviour
    {
        private TilesScriptableObject _tilesScriptableObject;
        private Tilemap _tilemap;
        private GridSystem _gridSystem;

        private void Awake()
        {
            _tilemap = GetComponentInChildren<Tilemap>();
        }

        public void Initiate(TilesScriptableObject tilesScriptableObject, GridSystem gridSystem)
        {
            _tilesScriptableObject = tilesScriptableObject;
            _tilemap.size = new Vector3Int(gridSystem.Width, gridSystem.Height, 0);
            _gridSystem = gridSystem;
            _gridSystem.ShapeSpawn += OnShapeSpawn;
            _gridSystem.ShapeMove += OnShapeMove;
            _gridSystem.ShapeRotate += OnShapeRotated;
            _gridSystem.ShapeRemoved += OnShapeRemoved;
            _gridSystem.GridUpdated += OnGridSystemUpdated;
        }

        private void OnShapeMove(Vector2Int from, Vector2Int to, FallingShape fallingShape)
        {
            Tile tile = _tilesScriptableObject.GetTile(fallingShape.GetItemColor());
            Vector2Int[] centerOffsets = fallingShape.GetCenterOffsets();
            SetTiles(from, centerOffsets, null);
            SetTiles(to, centerOffsets, tile);
        }

        private void OnShapeSpawn(Vector2Int position, FallingShape fallingShape)
        {
            Tile tile = _tilesScriptableObject.GetTile(fallingShape.GetItemColor());
            Vector2Int[] centerOffsets = fallingShape.GetCenterOffsets();
            SetTiles(fallingShape.GetPosition(), centerOffsets, tile);
        }

        private void OnShapeRotated(ShapeRotation from, ShapeRotation to, FallingShape fallingShape)
        {
            Tile tile = _tilesScriptableObject.GetTile(fallingShape.GetItemColor());
            Vector2Int position = fallingShape.GetPosition();
            SetTiles(position, fallingShape.GetCenterOffsets(from), null);
            SetTiles(position, fallingShape.GetCenterOffsets(to), tile);
        }
        private void OnShapeRemoved(Vector2Int position, FallingShape fallingShape)
        {
            SetTiles(position, fallingShape.GetCenterOffsets(), null);
        }

        private void OnGridSystemUpdated()
        {
            for (int i = 0; i < _gridSystem.Height; i++)
            {
                for (int j = 0; j < _gridSystem.Width; j++)
                {
                    GridItem gridItem = _gridSystem.GetGridItem(i, j);

                    var vector3IntPosition = new Vector3Int(j, i);
                    
                    _tilemap.SetTile(vector3IntPosition, gridItem.IsOccupied ? _tilesScriptableObject.GetTile(gridItem.ItemColor) : null);
                }
            }
        }

        private void SetTiles(Vector2Int center, Vector2Int[] offsets, Tile tile)
        {
            foreach (Vector2Int currentPosition in offsets)
            {
                _tilemap.SetTile((Vector3Int)(center + currentPosition), tile);
            }
        }

        private void OnDestroy()
        {
            if (_gridSystem != null)
            {
                _gridSystem.ShapeSpawn -= OnShapeSpawn;
                _gridSystem.ShapeMove -= OnShapeMove;
                _gridSystem.ShapeRotate -= OnShapeRotated;
            }
        }
    }
}
