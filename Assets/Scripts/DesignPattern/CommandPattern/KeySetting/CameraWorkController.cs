using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CameraWorkController : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    #region Serialized Field
    [Header("Camera Settings")]
    [SerializeField] private Transform cam = null;
    [SerializeField] private Transform lookAtTarget = null;
    [SerializeField] private Volume volume = null;

    [Header("Focus Settings")]
    [Range(0, 5f)]
    [SerializeField] private float focusDelay = 0.3f;

    [Header("Motion Settings")]
    [SerializeField] private float rotSpeed = 3f;
    [SerializeField] private AnimationCurvesPreset curves = null;
    #endregion

    #region Private Field
    private DepthOfField dof = null;
    private bool isCoroutineRunning = false;
    private bool isDragging = false;
    private float rotVelocityX = 0f;
    private float rotVelocityY = 0f;
    #endregion

    private void Awake()
    {
        InitCameraSetting();
    }

    #region Initialize
    private void InitCameraSetting()
    {
        volume.profile.TryGet(out dof);
    }
    #endregion

    #region Pointer Callbacks
    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
    }
    #endregion

    #region Camera Focus
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

    private void Update()
    {
        StartCoroutine(Focus());
        SurroudMovement();
    }

    #region Camera Movements
    private void SurroudMovement()
    {
        if (Input.GetMouseButtonDown(2))
            isDragging = true;

        if (Input.GetMouseButton(2))
        {
            var rotDeltaX = Input.GetAxis("Mouse X") * rotSpeed;
            var rotDeltaY = Input.GetAxis("Mouse Y") * rotSpeed;

            rotVelocityX = Mathf.Lerp(rotVelocityX, rotDeltaX, Time.deltaTime);
            rotVelocityY = Mathf.Lerp(rotVelocityY, rotDeltaY, Time.deltaTime);

            var rotAngleX = Quaternion.Euler(Vector3.up * -rotVelocityX);
            var rotAngleY = Quaternion.Euler(Vector3.right * rotVelocityY);

            var lookDir = (cam.position - lookAtTarget.position).normalized;
            var rotatedDir = rotAngleX * rotAngleY * lookDir;
            cam.transform.position = lookAtTarget.position + rotatedDir * 3;
            cam.transform.forward = -lookDir;
        }

        if (Input.GetMouseButtonUp(2))
            isDragging = false;
    }
    #endregion
}
