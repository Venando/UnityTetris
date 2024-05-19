using Tetris.Board;
using Tetris.Data;
using Tetris.Scriptables;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tetris.GridDisplay
{
    public class NextShapePreviewGrid : MonoBehaviour
    {
        private TilesScriptableObject _tilesScriptableObject;
        private Tilemap _tilemap;
        private ShapesProvider _shapesProvider;
        
        private void Awake()
        {
            _tilemap = GetComponentInChildren<Tilemap>();
            _tilemap.size = new Vector3Int(5, 6);
        }

        public void Initiate(ShapesProvider shapesProvider, TilesScriptableObject tilesScriptableObject)
        {
            _tilesScriptableObject = tilesScriptableObject;
            _shapesProvider = shapesProvider;
            _shapesProvider.NextShapeUpdated += ShapesProviderOnNextShapeUpdated;
            DisplayShape(_shapesProvider.PeekNextShape);
        }

        private void ShapesProviderOnNextShapeUpdated(RotatableShapeData nextShape)
        {
            DisplayShape(nextShape);
        }

        private void DisplayShape(RotatableShapeData shapeData)
        {
            ClearTilemap();
            SetTilemaps(shapeData);
        }

        private void ClearTilemap()
        {
            Vector3Int size = _tilemap.size;
            for (int i = 0; i < size.y; i++)
            {
                for (int j = 0; j < size.x; j++)
                {
                    _tilemap.SetTile(new Vector3Int(i, j, 0), null);
                }
            }
        }

        private void SetTilemaps(RotatableShapeData shapeData)
        {
            Vector3Int tilemapSize = _tilemap.size;
            var center = new Vector3Int(tilemapSize.x / 2, tilemapSize.y / 2, 0);
            Vector2Int[] centerOffsets = shapeData.GetPositions(ShapeRotation.Up);
            
            foreach (Vector2Int centerOffset in centerOffsets)
            {
                _tilemap.SetTile(center + (Vector3Int)centerOffset, _tilesScriptableObject.GetTile(ItemColor.Red));
            }
        }

        private void OnDestroy()
        {
            if (_shapesProvider != null)
            {
                _shapesProvider.NextShapeUpdated -= ShapesProviderOnNextShapeUpdated;
            }
        }
    }
}
