using Tetris.Data;
using UnityEngine;

namespace Tetris.Scriptables
{
    [CreateAssetMenu]
    public class ShapesScriptableObject : ScriptableObject
    {
        [SerializeField] private ShapeData[] _shapesData;

        public ShapeData[] GetShapes()
        {
            return _shapesData;
        }
    }
}