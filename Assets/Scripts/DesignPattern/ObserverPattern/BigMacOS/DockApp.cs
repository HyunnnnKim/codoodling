using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BigMacOS
{
    public class DockApp : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        #region Serialized Field
        [Header("Content Panel")]
        [SerializeField] private Transform contentHolder = null;
        [SerializeField] private GameObject contentPanelPrefab = null;
        #endregion

        #region Private Field
        private GameObject panel = null;
        #endregion

        #region Delegates
        public static event Action<GameObject> OnDockAppClick = null;
        #endregion

        private void Awake()
        {
            if (contentPanelPrefab == null) return;
            panel = Instantiate(contentPanelPrefab, contentHolder);
        }

        #region Pointer Callbacks
        public void OnPointerEnter(PointerEventData eventData)
        {

        }

        public void OnPointerExit(PointerEventData eventData)
        {

        }

        public void OnPointerDown(PointerEventData eventData)
        {
            
        }

        public void OnPointerUp(PointerEventData eventData)
        {

        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnDockAppClick?.Invoke(panel);
        }
        #endregion
    }
}
