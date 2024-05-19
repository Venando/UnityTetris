using System;
using System.Collections;
using Tetris.Scriptables;
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
        private Coroutine _continuesMovementInputCoroutine;
        private WaitForSeconds _autoMoveWaiter;

        public void Initiate(GameSettingsScriptableObject gameSettings)
        {
            _autoMoveWaiter = new WaitForSeconds(gameSettings.AutoMoveTimeDelay);
        }

        private void Update()
        {
            UpdateVerticalInput();
            UpdateHorizontalInput();
        }

        private void UpdateVerticalInput()
        {
            float vertical = Input.GetAxisRaw("Vertical");
            UpdateRotationInput(vertical);
            UpdateFastMode(vertical);
        }

        private void UpdateRotationInput(float vertical)
        {
            if (Input.GetButtonDown("Vertical") && vertical > 0)
            {
                RotateShape?.Invoke();
            }
        }

        private void UpdateFastMode(float vertical)
        {
            bool isFastMode = vertical < 0;

            if (isFastMode != _isFastMode)
            {
                _isFastMode = isFastMode;
                FastModeSwitch?.Invoke(isFastMode);
            }
        }

        private void UpdateHorizontalInput()
        {
            if (Input.GetButtonDown("Horizontal"))
            {
                ExecuteMovementInput();
                _continuesMovementInputCoroutine = StartCoroutine(ContinuesMovementInput());
            }
            else if (Input.GetButtonUp("Horizontal"))
            {
                StopCoroutine(_continuesMovementInputCoroutine);
            }
        }

        private IEnumerator ContinuesMovementInput()
        {
            while (true)
            {
                yield return _autoMoveWaiter;
                ExecuteMovementInput();
            }
        }

        private void ExecuteMovementInput()
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