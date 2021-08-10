using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

namespace Gameboy
{
    public class ShowcaseController : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        #region Serialized Field
        [Header("Showcase Settings")]
        [SerializeField] private Transform showcaseObject = null;
        [SerializeField] private float rotSpeed = 6f;
        [SerializeField] private float rotDamping = 2f;

        [Header("Camera Settings")]
        [SerializeField] private Transform showcaseCam = null;
        [SerializeField] private Volume volume = null;
        #endregion

        #region Private Field
        private bool isDragging = false;
        private float rotVelocityX = 0f;
        private float rotVelocityY = 0f;
        #endregion

        private void Awake()
        {
            showcaseObject.parent = transform;
        }

        #region Drag Callback
        public void OnBeginDrag(PointerEventData eventData)
        {
            isDragging = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            var rodDeltaX = eventData.delta.x * rotSpeed * Time.deltaTime;
            var rotDeltaY = eventData.delta.y * rotSpeed * Time.deltaTime;

            rotVelocityX = Mathf.Lerp(rotVelocityX, rodDeltaX, Time.deltaTime);
            rotVelocityY = Mathf.Lerp(rotVelocityY, rotDeltaY, Time.deltaTime);

            showcaseObject.Rotate(Vector3.up, -rotVelocityX, Space.Self);
            showcaseObject.Rotate(Vector3.forward, -rotVelocityY, Space.Self);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            isDragging = false;
        }
        #endregion

        private void Update()
        {
            VelocityXMovement();
            VelocityYMovement();
        }

        #region Showcase Movement
        private void VelocityXMovement()
        {
            if (!isDragging && !Mathf.Approximately(rotVelocityX, 0))
            {
                float deltaVelocity = Mathf.Min(
                    Mathf.Sign(rotVelocityX) * rotDamping * Time.deltaTime,
                    Mathf.Sign(rotVelocityX) * rotVelocityX
                );
                rotVelocityX -= deltaVelocity;
                showcaseObject.Rotate(Vector3.up, -rotVelocityX, Space.Self);
            }
        }

        private void VelocityYMovement()
        {
            if (!isDragging && !Mathf.Approximately(rotVelocityY, 0))
            {
                float deltaVelocity = Mathf.Min(
                    Mathf.Sign(rotVelocityY) * rotDamping * Time.deltaTime,
                    Mathf.Sign(rotVelocityY) * rotVelocityY
                );
                rotVelocityY -= deltaVelocity;
                showcaseObject.Rotate(Vector3.forward, -rotVelocityY, Space.Self);
            }
        }
        #endregion
    }
}
