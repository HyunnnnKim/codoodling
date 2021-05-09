using Unity.Collections;
using UnityEngine;

namespace Collie
{
    public class RigidbodyController : MonoBehaviour
    {
        #region Serialize Field
        [Space, Header("Input Data")]
        [SerializeField]
        private MovementInputData movementInputData = null;
        [SerializeField]
        private CameraInputData cameraInputData = null;
        [SerializeField]
        private InteractionInputData interactionInputData = null;

        [Space, Header("Movement Settings")]
        [SerializeField]
        private float walkSpeed = 2f;
        [SerializeField]
        private float runSpeed = 4f;
        [SerializeField, ReadOnly]
        private float _currentSpeed = 0f;
        [SerializeField]
        private float movementForce = 50f;
        [SerializeField, Range(0f, 1f)]
        private float backwardMovementSpeedPercent = 0.5f;
        [SerializeField, Range(0f, 1f)]
        private float sideMovementSpeedPercent = 0.75f;

        [Space, Header("Jump Settings")]
        [SerializeField]
        private float jumpForce = 60f;
        [SerializeField]
        private LayerMask groundLayer = ~0;
        [SerializeField, Range(0.01f, 1f)]
        private float raySphereRadius = 0.1f;
        [SerializeField, Range(0f, 1f)]
        private float rayLength = 0.1f;

        [Space, Header("Smooth Settings")]
        [SerializeField, Range(1f, 100f)]
        private float smoothInputSpeed = 5f;
        [SerializeField, Range(1f, 100f)]
        private float smoothDirectionSpeed = 5f;
        [SerializeField]
        private float smoothRotationTime = 0.1f;
        #endregion

        #region Private Field
        private Vector2 _inputVec = Vector2.zero;
        private Vector2 _smoothInputVec = Vector2.zero;

        private Vector3 _dir = Vector3.zero;
        private Vector3 _smoothDir = Vector2.zero;
        private float _targetAngle = 0f;
        private float _angle = 0f;
        private Vector3 _moveDir = Vector3.zero;
        private bool _isGrounded = false;
        private float smoothRotationSpeed = 0f;

        private Rigidbody _rb = null;
        private Transform _cameraTransform = null;
        #endregion

        private void Awake()
        {
            GetComponents();
            InitVariables();
        }

        #region Initialize
        private void GetComponents()
        {
            _rb = this.GetComponentInChildren<Rigidbody>();
        }

        private void InitVariables()
        {
            Cursor.lockState = CursorLockMode.Locked;
            _cameraTransform = Camera.main.transform;
            rayLength += _rb.transform.position.y;
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
            _inputVec = movementInputData.MovementInput.normalized;
            _smoothInputVec = Vector2.Lerp(_smoothInputVec, _inputVec, Time.deltaTime * smoothInputSpeed);
        }

        private void SmoothDir()
        {
            _dir = Vector3.forward * _smoothInputVec.y + Vector3.right * _smoothInputVec.x;
            _smoothDir = Vector3.Lerp(_smoothDir, _dir, Time.deltaTime * smoothDirectionSpeed);
        }
        #endregion

        #region Check State
        private void IsGrounded()
        {
            _isGrounded = Physics.SphereCast(_rb.transform.position, raySphereRadius, Vector3.down, out RaycastHit _hitInfo, rayLength, groundLayer);
            Debug.DrawRay(_rb.transform.position, Vector3.down * rayLength, Color.red);
        }
        #endregion

        #region Calculate Data
        private void CalculateAngle()
        {
            _targetAngle = Mathf.Atan2(_smoothDir.x, _smoothDir.z) * Mathf.Rad2Deg + _cameraTransform.eulerAngles.y;
            _angle = Mathf.SmoothDampAngle(_rb.transform.eulerAngles.y, _targetAngle, ref smoothRotationSpeed, smoothRotationTime);
        }

        private void CalculateMoveDir()
        {
            _moveDir = Quaternion.Euler(0f, _targetAngle, 0f) * Vector3.forward;
        }

        private void CalculateSpeed()
        {
            _currentSpeed = movementInputData.IsRunning && _isGrounded ? runSpeed : walkSpeed;
            _currentSpeed = !movementInputData.HasMovementInput ? 0f : _currentSpeed;
            _currentSpeed = movementInputData.MovementInput.y == -1 ? _currentSpeed * backwardMovementSpeedPercent : _currentSpeed;
            _currentSpeed = movementInputData.MovementInput.x != 0 && movementInputData.MovementInput.y == 0 ? _currentSpeed * sideMovementSpeedPercent : _currentSpeed;
        }
        #endregion

        private void FixedUpdate()
        {
            if (movementInputData.HasMovementInput)
            {
                ApplyRotation(_angle);
                ApplyMovement(_moveDir * _currentSpeed, movementForce, ForceMode.Force);
            }

            if (movementInputData.JumpPressed)
            {
                ApplyJump(jumpForce, ForceMode.Impulse);
            }
        }

        #region Physics Movement
        private void ApplyMovement(Vector3 targetVelocity, float force, ForceMode forceMode)
        {
            _rb.AddForce(targetVelocity * force, forceMode);
        }

        private void ApplyJump(float force, ForceMode forceMode)
        {
            if (_isGrounded)
            {
                _rb.AddForce(force * _rb.mass * Time.deltaTime * Vector3.up * jumpForce, forceMode);
            }
        }

        private void ApplyRotation(float angle)
        {
            _rb.transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }          
        #endregion
    }
}