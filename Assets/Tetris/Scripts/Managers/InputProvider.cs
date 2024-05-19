using System;
using UnityEngine;

namespace Tetris.Managers
{
    public class InputProvider : MonoBehaviour, IInputProvider
    {
        public bool IsFastMode => _isFastMode;
        
        public event Action RotateShape;
        public event Action MoveLeft;
        public event Action MoveRight;
        public event Action<bool> FastModeSwitch;

        private bool _isFastMode = false;
        
        private void Update()
        {
            float vertical = Input.GetAxisRaw("Vertical");

            if (Input.GetButtonDown("Vertical") && vertical > 0)
            {
                RotateShape?.Invoke();
            }

            bool isFastMode = vertical < 0;
            if (isFastMode != _isFastMode)
            {
                _isFastMode = isFastMode;
                FastModeSwitch?.Invoke(isFastMode);
            }

            if (Input.GetButtonDown("Horizontal"))
            {
                float horizontal = Input.GetAxisRaw("Horizontal");
                
                if (horizontal > 0)
                {
                    MoveRight?.Invoke(); 
                }
                else if (horizontal < 0)
                {
                    MoveLeft?.Invoke();
                }
            }
        }
    }
}