using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitPhysics : MonoBehaviour
{
    #region Serialized Field
    [SerializeField] private Rigidbody target = null;
    #endregion

    private Rigidbody rigidbody = null;

    private void Start()
    {
        Setup();
    }

    #region Initialize
    private void Setup()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    #endregion

    private void FixedUpdate()
    {
        rigidbody.GravetationalPull(target, ForceMode.Force);
    }

    #region Orbit

    #endregion
}

public static class NewtonsLaw
{
    public static void GravetationalPull(this Rigidbody selfRB, Rigidbody targetRB, ForceMode forceMode = ForceMode.Force)
    {
        Vector3 dir = targetRB.position - selfRB.position;
        Vector3 gravityDir = dir.normalized;
        float dst = dir.magnitude;
        float gravity = 6.7f * (selfRB.mass * targetRB.mass * 80) / (dst * dst);
        Vector3 gravityVector = gravityDir * gravity;
        selfRB.AddForce(gravityVector, forceMode);
    }
}
