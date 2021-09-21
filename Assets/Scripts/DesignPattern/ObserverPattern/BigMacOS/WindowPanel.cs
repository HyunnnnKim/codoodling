using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BigMacOS
{
    public class WindowPanel : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        #region Private Field
        private static List<Canvas> contentPanels = new List<Canvas>();
        private static List<Canvas> activeContentPanels = new List<Canvas>();

        private RectTransform rectTransform = null;
        private Canvas rootCanvas = null;
        private Canvas panel = null;
        #endregion

        private void OnEnable()
        {
            rectTransform = GetComponent<RectTransform>();
            rootCanvas = GetComponentInParent<Canvas>();
            panel = GetComponentInChildren<Canvas>();

            DockApp.OnDockAppClick += EnablePanel;

            AddPanel();
        }

        #region Initialize
        private void AddPanel()
        {
            if (panel == null) return;
            panel.enabled = false;
            contentPanels.Add(panel);
        }
        #endregion

        #region Subscribed
        /// <summary>
        /// 
        /// </summary>
        private void EnablePanel(GameObject selectedPanel)
        {
            if (selectedPanel != gameObject) return;

            if (!panel.enabled)
            {
                panel.enabled = true;
                activeContentPanels.Add(panel);
            }

            int sortOrder = 1;
            int panelSelfIndex = 0;
            for (int i = 0; i < activeContentPanels.Count; i++)
            {
                if (activeContentPanels[i] != panel)
                {
                    activeContentPanels[i].sortingOrder = sortOrder;
                    sortOrder++;
                }
                else
                {
                    activeContentPanels[i].sortingOrder = activeContentPanels.Count;
                    panelSelfIndex = i;
                }
            }
            activeContentPanels.RemoveAt(panelSelfIndex);
            activeContentPanels.Add(panel);
        }
        #endregion

        #region Pointer Callbacks
        public void OnBeginDrag(PointerEventData eventData)
        {

        }

        public void OnDrag(PointerEventData eventData)
        {
            rectTransform.anchoredPosition += eventData.delta / rootCanvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {

        }

        public void OnPointerDown(PointerEventData eventData)
        {

        }
        #endregion

        private void OnDisable()
        {
            DockApp.OnDockAppClick -= EnablePanel;

            RemovePanel();
        }

        #region Terminate
        private void RemovePanel()
        {
            if (contentPanels.Contains(panel))
                contentPanels.Remove(panel);
        }
        #endregion
    }
}
