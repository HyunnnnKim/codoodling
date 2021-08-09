using UnityEngine;

public class ObjectCreator : MonoBehaviour
{
    #region Serialized Field
    [SerializeField] private Camera mainCam;
    [SerializeField] private Transform prefab;
    #endregion

    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {
        SpawnObjectWithRay();
    }

    #region Object Generator
    private void SpawnObjectWithRay()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity))
            {
                Color color = new Color(Random.Range(0.5f, 1f), Random.Range(0.5f, 1f), Random.Range(0.5f, 1f));
                IObjectPlaceCommand command = new PlaceCubeCommand(hitInfo.point, color, prefab);
                ObjectPlaceCommandInvoker.AddCommand(command);
            }
        }
    }
    #endregion
}
