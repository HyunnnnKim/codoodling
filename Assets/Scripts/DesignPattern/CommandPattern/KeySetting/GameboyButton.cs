using UnityEngine;
using UnityEngine.EventSystems;

namespace Gameboy
{
    public class GameboyButton : GameboyPointerBase
    {
        #region Serialized Field
        [SerializeField] private GameboyButtonType buttonType = GameboyButtonType.None;
        #endregion

        #region Delegates
        public delegate void GameboyButtonDelegate(GameboyButtonType buttonType);
        public static event GameboyButtonDelegate ButtonAction = null;
        #endregion

        #region Private Field
        private Collider buttonCollider = null;
        #endregion

        private void Start()
        {
            buttonCollider = GetComponent<Collider>();
        }

        #region Initialize

        #endregion

        #region Button Feedbacks
        protected override void ButtonEnterMovement(PointerEventData eventData)
        {
            if (gameObject.GetInstanceID() != eventData.selectedObject.GetInstanceID()) return;


        }

        protected override void ButtonExitMovement(PointerEventData eventData)
        {
            if (gameObject.GetInstanceID() != eventData.selectedObject.GetInstanceID()) return;


        }

        protected override void ButtonDownMovement(PointerEventData eventData)
        {
            if (gameObject.GetInstanceID() != eventData.selectedObject.GetInstanceID()) return;

            ButtonPress();
            ButtonAction?.Invoke(buttonType);
        }

        protected override void ButtonUpMovement(PointerEventData eventData)
        {
            if (gameObject.GetInstanceID() != eventData.selectedObject.GetInstanceID()) return;

            ButtonRelease();
            ButtonAction?.Invoke(buttonType);
        }
        #endregion

        #region Button Movements
        private void ButtonPress()
        {
            var buttonPos = transform.position;
            buttonPos.z -= buttonCollider.bounds.min.z * 0.09f;
            transform.position = buttonPos;
        }

        private void ButtonRelease()
        {

        }
        #endregion
    }
}
