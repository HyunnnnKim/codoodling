using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizeRandom : MonoBehaviour
{
    [SerializeField]
    private float mean = 0f;
    [SerializeField]
    private float standardDeviation = 0f;
    [SerializeField]
    private int elementNum = 100;
    [SerializeField]
    private float offset = 3f;

    private int _count = 0;

    private void Update()
    {
        StartCoroutine(VisualGaussian());
    }

    private IEnumerator VisualGaussian()
    {
        if (_count >= elementNum) yield break;

        while (_count < elementNum)
        {
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.localScale = Vector3.one * 0.1f;
            var pos = sphere.transform.position;
            float x = Gaussian.GenerateGaussian(mean, standardDeviation);
            float z = Gaussian.GenerateGaussian(mean, standardDeviation);
            pos.x += x; pos.y = 1f; pos.z += z;
            sphere.transform.position = pos;
            _count++;
            yield return null;
        }
    }
}
