using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitPhysics : MonoBehaviour
{
    #region Serialized Field
    [SerializeField] private Rigidbody targetRb = null;
    [SerializeField] private float gravityValue = 6.7f;
    #endregion

    private Rigidbody rb = null;

    private void Start()
    {
        Setup();
    }

    #region Initialize
    private void Setup()
    {
        rb = GetComponent<Rigidbody>();
    }
    #endregion

    private void FixedUpdate()
    {
        PhysicalOrbit();
    }

    #region Orbit
    private void PhysicalOrbit()
    {
        if (rb == null || targetRb == null) return;
        rb.GravetationalPull(targetRb, gravityValue, ForceMode.Acceleration);
        rb.AddForce(rb.transform.forward, ForceMode.Acceleration);
    }
    #endregion
}
