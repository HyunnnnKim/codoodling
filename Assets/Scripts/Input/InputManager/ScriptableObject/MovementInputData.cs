using UnityEngine;

namespace Collie
{
    [CreateAssetMenu]
    public class MovementInputData : ScriptableObject
    {
        #region Data
        private Vector2 _movementInput;
        private bool _isRunning;
        private bool _runPressed;
        private bool _runReleased;
        private bool _jumpPressed;
        #endregion

        #region Properties
        public Vector2 MovementInput => _movementInput;
        public bool HasMovementInput => _movementInput != Vector2.zero;

        public float InputVecX { set => _movementInput.x = value; }
        public float InputVecY { set => _movementInput.y = value; }

        public bool IsRunning { get => _isRunning; set => _isRunning = value; }

        public bool RunPressed { get => _runPressed; set => _runPressed = value; }
        public bool RunReleased { get => _runReleased; set => _runReleased = value; }

        public bool JumpPressed { get => _jumpPressed; set => _jumpPressed = value; }
        #endregion

        #region Reset
        public void ResetInput()
        {
            _movementInput = Vector2.zero;

            _isRunning = false;

            _runPressed = false;
            _runReleased = false;

            _jumpPressed = false;
        }
        #endregion
    }
}
