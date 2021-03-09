using UnityEngine;

namespace Collie
{
    [CreateAssetMenu]
    public class InteractionInputData : ScriptableObject
    {
        #region Data
        private bool _interactPressed;
        private bool _interactReleased;
        #endregion

        #region Properties
        public bool InteractPressed { get => _interactPressed; set => _interactPressed = value; }
        public bool InteractReleased { get => _interactReleased; set => _interactReleased = value; }
        #endregion

        #region Reset
        public void ResetInput()
        {
            _interactPressed = false;
            _interactReleased = false;
        }
        #endregion
    }
}
