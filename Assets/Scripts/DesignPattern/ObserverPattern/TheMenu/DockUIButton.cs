using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheMenu
{
    public class DockUIButton : BaseUIButton
    {
        #region Serialized Field
        [Header("Panel")]
        [SerializeField] Canvas contentPanel = null;
        #endregion

        #region Private Field
        private static List<DockUIButton> dockButtons = new List<DockUIButton>();
        #endregion

        #region UI

        #endregion
    }
}
