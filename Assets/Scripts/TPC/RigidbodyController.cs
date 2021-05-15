using Unity.Collections;
using UnityEngine;

namespace Collie
{
    public class RigidbodyController : MonoBehaviour
    {
        #region Serialize Field
        [Space, Header("Input Data")]
        [SerializeField] private MovementInputData movementInputData = null;
        [SerializeField] private CameraInputData cameraInputData = null;
        [SerializeField] private InteractionInputData interactionInputData = null;

        [Space, Header("Target")]
        [SerializeField] private Rigidbody rb = null;
        [SerializeField] private Transform cameraTransform = null;

        [Space, Header("Movement Settings")]
        [SerializeField] private float walkSpeed = 2f;
        [SerializeField] private float runSpeed = 4f;
        [ReadOnly]
        [SerializeField] private float currentSpeed = 0f;
        [SerializeField] private float movementForce = 50f;
        [Range(0f, 1f)]
        [SerializeField] private float backwardMovementSpeedPercent = 0.5f;
        [Range(0f, 1f)]
        [SerializeField] private float sideMovementSpeedPercent = 0.75f;

        [Space, Header("Jump Settings")]
        [SerializeField] private float jumpForce = 60f;
        [SerializeField] private LayerMask groundLayer = ~0;
        [Range(0.01f, 1f)]
        [SerializeField] private float raySphereRadius = 0.1f;
        [Range(0f, 1f)]
        [SerializeField] private float rayLength = 0.1f;

        [Space, Header("Smooth Settings")]
        [Range(1f, 100f)]
        [SerializeField] private float smoothInputSpeed = 5f;
        [Range(1f, 100f)]
        [SerializeField] private float smoothDirectionSpeed = 5f;
        [SerializeField] private float smoothRotationTime = 0.1f;
        #endregion

        #region Private Field
        private Vector2 inputVec = Vector2.zero;
        private Vector2 smoothInputVec = Vector2.zero;

        private Vector3 dir = Vector3.zero;
        private Vector3 smoothDir = Vector2.zero;
        private float targetAngle = 0f;
        private float angle = 0f;
        private Vector3 moveDir = Vector3.zero;
        private bool isGrounded = false;
        private float smoothRotationSpeed = 0f;
        #endregion

        private void Awake()
        {
            InitVariables();
        }

        #region Initialize
        private void InitVariables()
        {
            Cursor.lockState = CursorLockMode.Locked;
            cameraTransform = cameraTransform ?? Camera.main.transform;
            rayLength += rb.transform.position.y;
        }
        #endregion

        private void Update()
        {
            SmoothInput();
            SmoothDir();
            CalculateAngle();
            CalculateMoveDir();

            IsGrounded();
            CalculateSpeed();
        }

        #region Smooth Inputs
        private void SmoothInput()
        {
            inputVec = movementInputData.MovementInput.normalized;
            smoothInputVec = Vector2.Lerp(smoothInputVec, inputVec, Time.deltaTime * smoothInputSpeed);
        }

        private void SmoothDir()
        {
            dir = Vector3.forward * smoothInputVec.y + Vector3.right * smoothInputVec.x;
            smoothDir = Vector3.Lerp(smoothDir, dir, Time.deltaTime * smoothDirectionSpeed);
        }
        #endregion

        #region Check State
        private void IsGrounded()
        {
            isGrounded = Physics.SphereCast(rb.transform.position, raySphereRadius, Vector3.down, out RaycastHit _hitInfo, rayLength, groundLayer);
            Debug.DrawRay(rb.transform.position, Vector3.down * rayLength, Color.red);
        }
        #endregion

        #region Calculate Data
        private void CalculateAngle()
        {
            targetAngle = Mathf.Atan2(smoothDir.x, smoothDir.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            angle = Mathf.SmoothDampAngle(rb.transform.eulerAngles.y, targetAngle, ref smoothRotationSpeed, smoothRotationTime);
        }

        private void CalculateMoveDir()
        {
            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }

        private void CalculateSpeed()
        {
            currentSpeed = movementInputData.IsRunning && isGrounded ? runSpeed : walkSpeed;
            currentSpeed = !movementInputData.HasMovementInput ? 0f : currentSpeed;
            currentSpeed = movementInputData.MovementInput.y == -1 ? currentSpeed * backwardMovementSpeedPercent : currentSpeed;
            currentSpeed = movementInputData.MovementInput.x != 0 && movementInputData.MovementInput.y == 0 ? currentSpeed * sideMovementSpeedPercent : currentSpeed;
        }
        #endregion

        private void FixedUpdate()
        {
            if (movementInputData.HasMovementInput)
            {
                ApplyRotation(angle);
                ApplyMovement(moveDir * currentSpeed, movementForce, ForceMode.Force);
            }

            if (movementInputData.JumpPressed)
            {
                ApplyJump(jumpForce, ForceMode.Impulse);
            }
        }

        #region Physics Movement
        private void ApplyMovement(Vector3 targetVelocity, float force, ForceMode forceMode)
        {
            rb.AddForce(targetVelocity * force, forceMode);
        }

        private void ApplyJump(float force, ForceMode forceMode)
        {
            if (isGrounded)
            {
                rb.AddForce(force * rb.mass * Time.deltaTime * Vector3.up * jumpForce, forceMode);
            }
        }

        private void ApplyRotation(float angle)
        {
            rb.transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }          
        #endregion
    }
}