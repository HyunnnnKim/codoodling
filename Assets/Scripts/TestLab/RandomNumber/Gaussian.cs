using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Gaussian
{
    /// <summary>
    /// Marsaglia polar method
    /// </summary>
    /// <param name="mean"></param>
    /// <param name="stdDev"></param>
    /// <returns></returns>
    public static float GenerateGaussian(float mean, float stdDev)
    {
        float u = 0f, v = 0f, s = 0f;
        do
        {
            u = 2.0f * Random.value - 1.0f; // between -1.0 and 1.0
            v = 2.0f * Random.value - 1.0f;
            s = u * u + v * v;
        }
        while (s >= 1.0f || s == 0f);
        s = Mathf.Sqrt(-2.0f * Mathf.Log(s) / s);
        float standardNormalDistribution = v * s;
        Debug.Log(standardNormalDistribution);
        return mean + stdDev * standardNormalDistribution;
    }

    public static float generateNormalRandom(float mu, float sigma)
    {
        float rand1 = Random.Range(0.0f, 1.0f);
        float rand2 = Random.Range(0.0f, 1.0f);

        float n = Mathf.Sqrt(-2.0f * Mathf.Log(rand1)) * Mathf.Cos((2.0f * Mathf.PI) * rand2);

        return (mu + sigma * n);
    }
}
