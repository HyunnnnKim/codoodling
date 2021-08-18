using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundMovement : MonoBehaviour
{
    #region Serialized Field
    [SerializeField] private float scrollSpeed = 3f;
    [SerializeField] private List<Image> backgroundLayer = null;
    #endregion

    #region Private Field
    private Vector2 scrollVec = Vector2.zero;
    #endregion

    private void Update()
    {
        foreach (var layer in backgroundLayer)
        {
            scrollVec.x += scrollSpeed * Time.deltaTime;
            /* using layer.material won't work if the Image component is child to Mask Component. */
            layer.materialForRendering.SetTextureOffset("_MainTex", scrollVec);
        }
    }
}
