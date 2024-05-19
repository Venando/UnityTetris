using System;
using System.Collections.Generic;
using Tetris.Board;
using UnityEngine;
using UnityEngine.Tilemaps;
using TileData = Tetris.Board.TileData;

namespace Tetris.Scriptables
{
    [CreateAssetMenu]
    public class TilesScriptableObject : ScriptableObject
    {
        [SerializeField] private TileData[] _tilesData;

        public IReadOnlyList<TileData> GetTiles()
        {
            return _tilesData;
        }

        public Tile GetTile(ItemColor itemColor)
        {
            foreach (TileData tileData in _tilesData)
            {
                if (tileData.ItemColor == itemColor)
                {
                    return tileData.Tile;
                }
            }
            
            throw new NotSupportedException();
        } 
    }
}