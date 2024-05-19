using System;
using Tetris.Data;
using Tetris.Scriptables;
using Random = UnityEngine.Random;

namespace Tetris.Board
{
    public class ShapesProvider
    {
        public RotatableShapeData PeekNextShape { get; private set; }

        public event Action<RotatableShapeData> NextShapeUpdated;
        
        private RotatableShapeData[] _rotatableShapesData;

        public ShapesProvider(ShapesScriptableObject shapesScriptableObject)
        {
            InitRotatableShapes(shapesScriptableObject);
        }

        private void InitRotatableShapes(ShapesScriptableObject shapesScriptableObject)
        {
            ShapeData[] shapes = shapesScriptableObject.GetShapes();
            _rotatableShapesData = new RotatableShapeData[shapes.Length];
            for (var i = 0; i < shapes.Length; i++)
            {
                _rotatableShapesData[i] = new RotatableShapeData(shapes[i]);
            }

            PeekNextShape = GetRandomShapeData();
        }

        private RotatableShapeData GetRandomShapeData()
        {
            return _rotatableShapesData[Random.Range(0, _rotatableShapesData.Length)];
        }

        public RotatableShapeData GetNextShape()
        {
            RotatableShapeData returnShape = PeekNextShape;
            PeekNextShape = GetRandomShapeData();
            NextShapeUpdated?.Invoke(PeekNextShape);
            return returnShape;
        }
    }
}