using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundMovement : MonoBehaviour
{
    #region Serialized Field
    [SerializeField] private float scrollSpeed = 3f;
    [SerializeField] private List<BackgroundLayer> layers = null;
    [SerializeField] private Material material = null;
    #endregion

    private void Start()
    {
        InitMaterials();
    }

    #region Initialize
    private void InitMaterials()
    {
        foreach (var layer in layers)
        {
            layer.background.material = new Material(material);
        }
    }
    #endregion

    private void Update()
    {
        ParallaxMovement();
    }

    private void ParallaxMovement()
    {
        foreach (var layer in layers)
        {
            var offset = layer.background.materialForRendering.mainTextureOffset;
            offset.x += (scrollSpeed * Time.deltaTime) * layer.parallaxValue;
            /* using layer.material won't work if the Image component is child to the Mask Component. */
            layer.background.materialForRendering.mainTextureOffset = offset;
        }
    }
}

[Serializable]
public class BackgroundLayer
{
    public Image background = null;
    public float parallaxValue = 0.1f;
}