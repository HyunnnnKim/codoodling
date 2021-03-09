using UnityEngine;
using UnityEngine.Events;

public class AboutRotations : MonoBehaviour
{
    #region Events
    public UnityEvent bigCube = null;
    public UnityEvent smallCube = null;
    #endregion

    #region Serialized Field
    [Header("Position Value")]
    [SerializeField]
    private float moveSpeed = 60f;
    [Header("Rotate Value")]
    [SerializeField]
    private float xAngle = 0f;
    [SerializeField]
    private float yAngle = 0f;
    [SerializeField]
    private float zAngle = 0f;
    [SerializeField]
    private float rotSpeed = 3f;
    #endregion

    #region Private Field
    private Transform target = null;
    private float _smoothRotationSpeed = 0f;
    #endregion

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Target").transform;
    }

    private void Update()
    {
        bigCube?.Invoke();
        smallCube?.Invoke();
    }

    #region Rotations
    public void CubeRotate(GameObject obj)
    {
        obj.transform.Rotate(xAngle, yAngle, zAngle, Space.World);
    }

    public void CubeAngleAxis(GameObject obj)
    {
        obj.transform.rotation = Quaternion.AngleAxis(rotSpeed, Vector3.up);
    }

    public void CubeQmV(GameObject obj)
    {
        var dir = Quaternion.Euler(xAngle, yAngle, zAngle) * obj.transform.forward;
        obj.transform.forward = dir;
    }

    public void CubeQmQ(GameObject obj)
    {
        var rot = Quaternion.Euler(xAngle, yAngle, zAngle) * Quaternion.Euler(xAngle, yAngle, zAngle);
        obj.transform.rotation *= rot;
    }

    public void CubeLookRotation(GameObject obj)
    {
        var lookRot = Quaternion.LookRotation(target.position - obj.transform.position);
        var rot = Quaternion.Lerp(obj.transform.rotation, lookRot, rotSpeed * Time.deltaTime);
        obj.transform.rotation = rot;
    }

    public void CubeLookRotationY(GameObject obj)
    {
        var targetVec = Vector3.Scale(new Vector3(1, 0, 1), (target.position - obj.transform.position).normalized);
        var lookRot = Quaternion.LookRotation(targetVec);
        var rot = Quaternion.Lerp(obj.transform.rotation, lookRot, rotSpeed * Time.deltaTime);
        obj.transform.rotation = rot;
    }

    public void CubeAgleLookRotationY(GameObject obj)
    {
        var targetAngle = Mathf.Atan2(target.transform.position.x, target.transform.position.z) * Mathf.Rad2Deg;
        var angle = Mathf.SmoothDampAngle(obj.transform.eulerAngles.y, targetAngle, ref _smoothRotationSpeed, rotSpeed);
        obj.transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }

    public void CubeFollow(GameObject obj)
    {
        var angle = Mathf.SmoothDampAngle(obj.transform.eulerAngles.y, target.eulerAngles.y, ref _smoothRotationSpeed, rotSpeed);
        var targetPos = target.position;
        targetPos += Quaternion.Euler(0f, angle, 0f) * new Vector3(0f, 0f, -3f);
        obj.transform.position = Vector3.MoveTowards(obj.transform.position, targetPos, moveSpeed * Time.deltaTime);
        obj.transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }
    #endregion
}