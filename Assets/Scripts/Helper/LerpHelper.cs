using System.Collections;
using UnityEngine;

public static class LerpHelper
{
    public static IEnumerator Lerp(this Transform transform, Quaternion targetRot, float duration, AnimationCurve curve)
    {
        var elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            var lerpVal = Quaternion.Lerp(transform.localRotation, targetRot, curve.Evaluate(elapsedTime / duration));
            transform.localRotation = lerpVal;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localRotation = targetRot;
    }

    public static IEnumerator Lerp(this Transform transform, Vector3 targetPos, float duration, AnimationCurve curve)
    {
        var elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            var lerpVal = Vector3.Lerp(transform.localPosition, targetPos, curve.Evaluate(elapsedTime / duration));
            transform.localPosition = lerpVal;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = targetPos;
    }
}
