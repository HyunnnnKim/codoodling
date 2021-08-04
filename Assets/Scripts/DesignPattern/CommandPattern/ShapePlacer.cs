using System.Collections.Generic;
using UnityEngine;

public static class ShapePlacer
{
    #region
    private static List<Transform> shapes = null;

    #endregion

    public static void PlaceObject(Vector3 position, Color color, Transform obj)
    {
        Transform newObj = GameObject.Instantiate(obj, position, Quaternion.identity);
        newObj.GetComponentInChildren<MeshRenderer>().material.color = color;

        if (shapes == null)
            shapes = new List<Transform>();
        shapes.Add(newObj);
    }

    public static void RemoveObject(Vector3 position, Color color)
    {
        for (int i = 0; i < shapes.Count; i++)
        {
            if (shapes[i].position == position && shapes[i].GetComponentInChildren<MeshRenderer>().material.color == color)
            {
                GameObject.Destroy(shapes[i].gameObject);
                shapes.RemoveAt(i);
            }
        }
    }
}
