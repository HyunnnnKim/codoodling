using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Curves
{
    public static Vector3 LinearBezierCurves(Vector3 a, Vector3 b, float t)
    {
        return a + t * (b - a);
    }

    public static Vector3 QuadraticBezierCurves(Vector3 a, Vector3 b, Vector3 c, float t)
    {
        return Mathf.Pow(1 - t, 2) * a + 2 * (1 - t) * t * b + Mathf.Pow(t, 2) * c;
    }

    public static Vector3 CubicBezierCurves(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float t)
    {
        return Mathf.Pow(1 - t, 3) * a + 3 * Mathf.Pow(1 - t, 2) * t * b +
            3 * (1 - t) * Mathf.Pow(t, 2) * c + Mathf.Pow(t, 3) * d;
    }
}
