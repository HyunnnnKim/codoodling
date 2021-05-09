using UnityEngine;

namespace AboutTransform
{
    public static class AboutRotation
    {
        #region Revolution Functions
        public static Vector3 OrbitAround(this Vector3 point, Vector3 pivot, float distance, Quaternion angle)
        {
            return angle * (point - pivot).normalized * distance + pivot;
        }

        public static Vector3 OrbitAround(this Vector3 point, Vector3 pivot, float distance, Vector3 vec3)
        {
            return point.OrbitAround(pivot, distance, Quaternion.Euler(vec3));
        }

        public static void OrbitAround(this Transform orbitalObj, Vector3 pivot, float distance, Quaternion angle)
        {
            orbitalObj.localPosition = orbitalObj.localPosition.OrbitAround(pivot, distance, angle);
        }

        public static void OribitAround(this Transform orbitalObj, Vector3 pivot, float distance, Vector3 vec3)
        {
            orbitalObj.localPosition = orbitalObj.localPosition.OrbitAround(pivot, distance, Quaternion.Euler(vec3));
        }
        #endregion

        #region Rotation Functions
        
        #endregion
    }
}
