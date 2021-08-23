using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Gameboy
{
    public class CameraWorkController : MonoBehaviour, IBeginDragHandler, IEndDragHandler
    {
        #region Serialized Field
        [Header("Camera Settings")]
        [SerializeField] private Transform cam = null;
        [SerializeField] private Transform lookAtTarget = null;
        [SerializeField] private Volume volume = null;
        [Range(0, 5f)]
        [SerializeField] private float focusDelay = 0.3f;
        [SerializeField] private float rotSpeed = 3f;
        [SerializeField] private float focusRotSpeed = 3f;
        [SerializeField] private float focusRotDamping = 2f;
        [SerializeField] private AnimationCurvesPreset curves = null;
        #endregion

        #region Private Field
        private DepthOfField dof = null;
        private bool isCoroutineRunning = false;
        private bool isDragging = false;
        private bool isDraggingTarget = false;
        private float rotVelocityX = 0f;
        private float rotVelocityY = 0f;
        private Vector3 startingMousePos = Vector3.zero;

        private bool isGameFocusMode = false;
        private Vector3 originCamPos = Vector3.zero;
        private Vector3 gameFocusCamPos = Vector3.zero;
        #endregion

        private void Awake()
        {
            InitCameraSetting();
        }

        private void OnEnable()
        {
            GameboyButton.OnButtonDown += GameFocusMovement;
        }

        #region Initialize
        private void InitCameraSetting()
        {
            volume.profile.TryGet(out dof);
            originCamPos = cam.position;
            gameFocusCamPos = -lookAtTarget.right * 1.8f + Vector3.up * 0.18f;
        }
        #endregion

        #region Pointer Callbacks
        public void OnBeginDrag(PointerEventData eventData)
        {
            isDraggingTarget = true;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            isDraggingTarget = false;
        }
        #endregion

        private void Update()
        {
            StartCoroutine(Focus());
            BeginSurroudMovement();
        }

        #region Camera Focus
        /// <summary>
        /// 
        /// </summary>
        private IEnumerator Focus()
        {
            if (!isDragging) yield break;
            if (isCoroutineRunning) yield break;
            isCoroutineRunning = true;

            var ray = new Ray(cam.position, cam.forward);
            if (Physics.Raycast(ray, out var hit))
            {
                var hitDst = Vector3.Distance(cam.position, hit.point);
                var timeElapsed = 0f;

                while (timeElapsed < focusDelay)
                {
                    var lerpVal = Mathf.Lerp(dof.focusDistance.value, hitDst,
                        curves.EaseInOut.Evaluate(timeElapsed / focusDelay));
                    dof.focusDistance.value = lerpVal;
                    timeElapsed += Time.deltaTime;
                    yield return null;
                }
                dof.focusDistance.value = hitDst;
            }
            isCoroutineRunning = false;
        }
        #endregion

        #region Camera Movements
        /// <summary>
        /// 
        /// </summary>
        private void BeginSurroudMovement()
        {
            if (isGameFocusMode == true) return;

            if (Input.GetMouseButtonDown(2))
            {
                isDragging = true;
                startingMousePos = Input.mousePosition;
            }
            if (Input.GetMouseButtonUp(2))
            {
                isDragging = false;
            }
            SurroundMovement(isDragging == true ? rotSpeed : focusRotSpeed);
        }

        private void SurroundMovement(float rotSpeed)
        {
            if (isDragging || isDraggingTarget)
            {
                var mouseDir = (Input.mousePosition - startingMousePos).normalized;
                var rotDeltaX = mouseDir.x * rotSpeed * Time.deltaTime;
                var rotDeltaY = mouseDir.y * rotSpeed * Time.deltaTime;
                rotVelocityX = Mathf.Lerp(rotVelocityX, rotDeltaX, Time.deltaTime);
                rotVelocityY = Mathf.Lerp(rotVelocityY, rotDeltaY, Time.deltaTime);
            }
            else if (!isDraggingTarget && !Mathf.Approximately(rotVelocityX, 0))
            {
                float deltaVelocityX = Mathf.Min(
                    Mathf.Sign(rotVelocityX) * focusRotDamping * Time.deltaTime,
                    Mathf.Sign(rotVelocityX) * rotVelocityX
                );
                float deltaVelocityY = Mathf.Min(
                    Mathf.Sign(rotVelocityY) * focusRotDamping * Time.deltaTime,
                    Mathf.Sign(rotVelocityY) * rotVelocityY
                );
                rotVelocityX -= deltaVelocityX;
                rotVelocityY -= deltaVelocityY;
            }

            var rotAngleX = Quaternion.Euler(Vector3.up * -rotVelocityX);
            var rotAngleY = Quaternion.Euler(Vector3.right * rotVelocityY);

            var lookDir = (cam.position - lookAtTarget.position).normalized;
            var rotatedDir = rotAngleX * rotAngleY * lookDir;
            cam.transform.position = lookAtTarget.position + rotatedDir * 3;
            cam.transform.forward = -lookDir;
        }

        /// <summary>
        /// 
        /// </summary>
        private void GameFocusMovement(GameboyButtonType buttonType)
        {
            if (buttonType != GameboyButtonType.Start) return;

            isGameFocusMode = !isGameFocusMode;
            StartCoroutine(cam.transform.LerpWorld(isGameFocusMode == true ? gameFocusCamPos : originCamPos, 1f, curves.EaseInOut));
        }
        #endregion

        private void OnDisable()
        {
            GameboyButton.OnButtonDown -= GameFocusMovement;
        }
    }
}
