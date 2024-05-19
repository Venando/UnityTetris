using System;
using UnityEngine;

namespace Tetris.Data
{
    [Serializable]
    public class ShapeData
    {
        public Vector2Int[] Offsets;

        public ShapeData(Vector2Int[] offsets)
        {
            Offsets = offsets;
        }
    }
}