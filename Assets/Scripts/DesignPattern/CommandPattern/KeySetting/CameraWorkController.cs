using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CameraWorkController : MonoBehaviour
{
    #region Serialized Field
    [Header("Camera Settings")]
    [SerializeField] private Transform cam = null;
    [SerializeField] private Volume volume = null;

    [Header("Focus Settings")]
    [Range(0, 5f)]
    [SerializeField] private float focusDelay = 0.3f;

    [Header("Motion Settings")]
    [SerializeField] private AnimationCurvesPreset curves = null;
    #endregion

    #region Private Field
    private DepthOfField dof = null;
    private bool isCoroutineRunning = false;
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

    private void Update()
    {
        StartCoroutine(Focus());
    }

    #region Camera Focus
    private IEnumerator Focus()
    {
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
}
