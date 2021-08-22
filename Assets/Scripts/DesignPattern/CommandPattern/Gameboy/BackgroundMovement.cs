using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gameboy
{
    public class BackgroundMovement : MonoBehaviour
    {
        #region Serialized Field
        [SerializeField] private float scrollSpeed = 3f;
        [SerializeField] private List<BackgroundLayer> layers = null;
        [SerializeField] private Material material = null;
        #endregion

        #region Privte Field
        private bool backgroundMoveState = false;
        #endregion

        private void OnEnable()
        {
            GameboyAxisButton.OnAxisButtonDown += AxisButtonDownMovement;
            GameboyAxisButton.OnAxisButtonUp += AxisButtonUpMovement;
        }

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

        #region Background Movement
        private void ParallaxMovement()
        {
            if (backgroundMoveState == false) return;

            foreach (var layer in layers)
            {
                var offset = layer.background.materialForRendering.mainTextureOffset;
                offset.x += (scrollSpeed * Time.deltaTime) * layer.parallaxValue;
                /* using layer.material won't work if the Image component is child to the Mask Component. */
                layer.background.materialForRendering.mainTextureOffset = offset;
            }
        }

        private void AxisButtonDownMovement(GameboyAxisButtonType butonDir)
        {
            backgroundMoveState = true;
        }

        private void AxisButtonUpMovement(GameboyAxisButtonType butonDir)
        {
            backgroundMoveState = false;
        }
        #endregion

        private void OnDisable()
        {
            GameboyAxisButton.OnAxisButtonDown -= AxisButtonDownMovement;
            GameboyAxisButton.OnAxisButtonUp -= AxisButtonUpMovement;
        }
    }
}

[Serializable]
public class BackgroundLayer
{
    public Image background = null;
    public float parallaxValue = 0.1f;
}