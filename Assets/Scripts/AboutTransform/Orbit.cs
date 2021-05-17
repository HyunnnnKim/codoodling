using UnityEngine;

public class Orbit : MonoBehaviour
{
    #region Serialzied Field
    [Header("Revolution")]
    [SerializeField] private Transform target = null;
    [SerializeField] private float dstFromTarget = 2f;
    [SerializeField] private Vector3 revolutionAngle = new Vector3(1, 0, 0);

    [Header("Rotation")]
    [SerializeField] private float rotSpeed = 1f;
    [SerializeField] private Vector3 rotAngle = new Vector3(1, 1, 0);
    #endregion

    private Vector3 dir = Vector3.zero;

    private void Start()
    {
        Setup();
    }

    #region Initialize
    private void Setup()
    {
        dir = (transform.position - target.position).normalized;
    }
    #endregion

    private void Update()
    {
        Rotation();
        Revolution();
    }

    #region Orbit Functions
    private void Rotation()
    {
        transform.Rotate(rotAngle, rotSpeed);
    }

    private void Revolution()
    {
        dir = Quaternion.Euler(revolutionAngle) * dir;
        transform.position = target.position + dir * dstFromTarget;
    }
    #endregion

}
