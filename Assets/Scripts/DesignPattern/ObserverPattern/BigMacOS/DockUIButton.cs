using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BigMacOS
{
    public class DockUIButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        #region Serialized Field
        [Header("Content Panel")]
        [SerializeField] private Transform contentHolder = null;
        [SerializeField] private GameObject contentPanelPrefab = null;
        #endregion

        #region Private Field
        private static List<Canvas> contentPanels = new List<Canvas>();

        private Canvas panel = null;
        #endregion

        private void Awake()
        {
            if (contentPanelPrefab == null) return;

            var contentPanel = Instantiate(contentPanelPrefab, contentHolder);
            panel = contentPanel.GetComponentInChildren<Canvas>();
            panel.enabled = false;
        }

        private void OnEnable()
        {
            contentPanels.Add(panel);
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
            EnableWindow();
        }

        public void OnPointerUp(PointerEventData eventData)
        {

        }
        #endregion

        #region Dock Button Functions
        /// <summary>
        /// 
        /// </summary>
        private void EnableWindow()
        {
            if (panel == null) return;

            if (panel.enabled)
            {
                for (int i = 0; i < contentPanels.Count; i++)
                {
                    if (contentPanels[i] != panel)
                        contentPanels[i].sortingOrder = i + 1;
                    else
                        i--;
                }
                panel.sortingOrder = contentPanels.Count;
            }
            else
            {
                panel.enabled = true;
            }
        }
        #endregion

        #region UI
        #endregion

        private void OnDisable()
        {
            if (contentPanels.Contains(panel))
                contentPanels.Remove(panel);
        }
    }
}
