using UnityEngine;

public static class NewtonsLaw
{
    public static void GravetationalPull(this Rigidbody selfRB, Rigidbody targetRB, float gravityValue, ForceMode forceMode = ForceMode.Force)
    {
        Vector3 dir = targetRB.position - selfRB.position;
        Vector3 gravityDir = dir.normalized;
        float dst = dir.magnitude;
        float gravity = gravityValue * (selfRB.mass * targetRB.mass * 80) / (dst * dst);
        Vector3 gravityVector = gravityDir * gravity;
        selfRB.AddForce(gravityVector, forceMode);
    }
}
