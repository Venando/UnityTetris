using System;
using Tetris.Board;
using UnityEngine;

namespace Tetris.Managers
{
    public class PlayerController : IDisposable
    {
        private readonly IInputProvider _inputProvider;
        private readonly GridSystem _gridSystem;
        
        public PlayerController(IInputProvider inputProvider, GridSystem gridSystem)
        {
            _inputProvider = inputProvider;
            _gridSystem = gridSystem;
            
            _inputProvider.RotateShape += OnRotateShape;
            _inputProvider.MoveLeft += OnMoveLeft;
            _inputProvider.MoveRight += OnMoveRight;
        }

        private void OnRotateShape()
        {
            _gridSystem.TryToRotateFallingShape();
        }

        private void OnMoveLeft()
        {
            _gridSystem.MoveFallingShape(Vector2Int.left);
        }

        private void OnMoveRight()
        {
            _gridSystem.MoveFallingShape(Vector2Int.right);
        }

        public void Dispose()
        {
            _inputProvider.RotateShape -= OnRotateShape;
            _inputProvider.MoveLeft -= OnMoveLeft;
            _inputProvider.MoveRight -= OnMoveRight;
        }
    }
}