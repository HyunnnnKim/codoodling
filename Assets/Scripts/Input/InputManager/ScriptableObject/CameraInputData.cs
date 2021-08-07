using UnityEngine;

namespace Collie
{
    [CreateAssetMenu]
    public class CameraInputData : ScriptableObject
    {
        #region Data
        private Vector2 _cameraInput;
        #endregion

        #region Properties
        public Vector2 CameraInput => _cameraInput;
        public bool HasCameraInput => _cameraInput != Vector2.zero;

        public float InputVecX { set => _cameraInput.x = value; }
        public float InputVecY { set => _cameraInput.y = value; }
        #endregion

        #region Reset
        public void ResetInput()
        {
            _cameraInput = Vector2.zero;
        }
        #endregion
    }
}

