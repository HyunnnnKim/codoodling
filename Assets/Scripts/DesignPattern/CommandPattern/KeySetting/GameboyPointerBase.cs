using UnityEngine;
using UnityEngine.EventSystems;

namespace Gameboy
{
    public class GameboyPointerBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        #region Pointer Callbacks
        public void OnPointerEnter(PointerEventData eventData)
        {
            ButtonEnterFeedback(eventData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ButtonExitFeedback(eventData);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            ButtonDownFeedback(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            ButtonUpFeedback(eventData);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            ButtonClickFeedback(eventData);
        }
        #endregion

        #region Button Feedbacks
        protected virtual void ButtonEnterFeedback(PointerEventData eventData) { }

        protected virtual void ButtonExitFeedback(PointerEventData eventData) { }

        protected virtual void ButtonDownFeedback(PointerEventData eventData) { }

        protected virtual void ButtonUpFeedback(PointerEventData eventData) { }

        protected virtual void ButtonClickFeedback(PointerEventData eventData) { }
        #endregion
    }

    public enum GameboyButtonType { None, A, B, Select, Start }
    public enum GameboyAxisButtonType { None, Up, Down, Left, Right }
}
