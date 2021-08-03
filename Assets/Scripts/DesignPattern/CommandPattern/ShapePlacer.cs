using UnityEngine;

public static class ShapePlacer
{
    public static void PlaceObject(Vector3 position, Color color, Transform obj)
    {
        Transform newObj = GameObject.Instantiate(obj, position, Quaternion.identity);
        newObj.GetComponentInChildren<MeshRenderer>().material.color = color;
    }
}
