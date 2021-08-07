using UnityEngine;

namespace Collie
{
    public class InputHandler : MonoBehaviour
    {
        #region Serialize Field
        [Space, Header("Input Data")]
        [SerializeField] private MovementInputData movementInputData = null;
        [SerializeField] private CameraInputData cameraInputData = null;
        [SerializeField] private InteractionInputData interactionInputData = null;

        [Space, Header("Key")]
        [SerializeField] private KeyCode runKey = KeyCode.LeftShift;
        [SerializeField] private KeyCode jumpKey = KeyCode.Space;
        [SerializeField] private KeyCode interactKey = KeyCode.E;
        #endregion

        void Start()
        {
            movementInputData.ResetInput();
            cameraInputData.ResetInput();
            interactionInputData.ResetInput();
        }

        void Update()
        {
            GetMovementInput();
            GetCameraInput();
            GetInteractionInput();
        }

        #region Input
        private void GetMovementInput()
        {
            movementInputData.InputVecX = Input.GetAxisRaw("Horizontal");
            movementInputData.InputVecY = Input.GetAxisRaw("Vertical");

            movementInputData.RunPressed = Input.GetKeyDown(runKey);
            movementInputData.RunReleased = Input.GetKeyUp(runKey);

            if (movementInputData.RunPressed)
                movementInputData.IsRunning = true;

            if (movementInputData.RunReleased)
                movementInputData.IsRunning = false;

            movementInputData.JumpPressed = Input.GetKeyDown(jumpKey);
        }

        private void GetCameraInput()
        {
            cameraInputData.InputVecX = Input.GetAxis("Mouse X");
            cameraInputData.InputVecY = Input.GetAxis("Mouse Y");
        }

        private void GetInteractionInput()
        {
            interactionInputData.InteractPressed = Input.GetKeyDown(interactKey);
            interactionInputData.InteractReleased = Input.GetKeyUp(interactKey);
        }
        #endregion
    }
}