using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TheMenu
{
    public class BaseUIButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        #region Events
        public static event Action<BaseUIButton> OnUIButtonDown = null;
        public static event Action<BaseUIButton> OnUIButtonUp = null;
        public static event Action<BaseUIButton> OnUIButtonHoverIn = null;
        public static event Action<BaseUIButton> OnUIButtonHoverOut = null;
        public static event Action<BaseUIButton> OnUIButtonClick = null;
        #endregion

        #region Pointer Callbacks
        public void OnPointerDown(PointerEventData eventData)
        {
            OnUIButtonDown?.Invoke(this);
            UIPointerDown();
            AudioPointerDown();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnUIButtonUp?.Invoke(this);
            UIPointerUp();
            AudioPointerUp();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            OnUIButtonHoverIn?.Invoke(this);
            UIPointerEnter();
            AudioPointerEnter();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            OnUIButtonHoverOut(this);
            UIPointerExit();
            AudioPointerExit();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnUIButtonClick(this);
            UIPointerClick();
            AudioPointerClick();
        }
        #endregion

        #region UI
        protected virtual void UIPointerDown() { }
        protected virtual void UIPointerUp() { }
        protected virtual void UIPointerEnter() { }
        protected virtual void UIPointerExit() { }
        protected virtual void UIPointerClick() { }
        #endregion

        #region Audio
        protected virtual void AudioPointerDown() { }
        protected virtual void AudioPointerUp() { }
        protected virtual void AudioPointerEnter() { }
        protected virtual void AudioPointerExit() { }
        protected virtual void AudioPointerClick() { }
        #endregion
    }
}
