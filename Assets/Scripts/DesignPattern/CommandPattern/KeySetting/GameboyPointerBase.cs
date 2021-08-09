using UnityEngine;
using UnityEngine.EventSystems;

namespace Gameboy
{
    public class GameboyPointerBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        #region Pointer Callbacks
        public void OnPointerEnter(PointerEventData eventData)
        {
            eventData.selectedObject = gameObject;
            ButtonEnterMovement(eventData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            eventData.selectedObject = gameObject;
            ButtonExitMovement(eventData);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            eventData.selectedObject = gameObject;
            ButtonDownMovement(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            eventData.selectedObject = gameObject;
            ButtonUpMovement(eventData);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            eventData.selectedObject = gameObject;
            ButtonClickMovement(eventData);
        }
        #endregion

        #region Button Feedbacks
        protected virtual void ButtonEnterMovement(PointerEventData eventData) { }

        protected virtual void ButtonExitMovement(PointerEventData eventData) { }

        protected virtual void ButtonDownMovement(PointerEventData eventData) { }

        protected virtual void ButtonUpMovement(PointerEventData eventData) { }

        protected virtual void ButtonClickMovement(PointerEventData eventData) { }
        #endregion
    }

    public enum GameboyButtonType { None, A, B, Select, Start }
    public enum GameboyAxisType { None, Up, Down, Left, Right }
}
