using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundMovement : MonoBehaviour
{
    #region Serialized Field
    [SerializeField] private float scrollSpeed = 6f;
    [SerializeField] private List<Image> backgroundLayer = null;
    #endregion

    public Vector2 scrollVec = Vector2.zero;

    private void Update()
    {
        foreach (var layer in backgroundLayer)
        {
            scrollVec.x += scrollSpeed * Time.deltaTime;
            layer.material.mainTextureOffset = scrollVec;
            //mat.SetVector("_ScrollSpeed", scrollVec);
        }
    }
}
