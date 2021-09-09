using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheMenu
{
    public class SideUIButton : BaseUIButton
    {
        #region Serialized Field
        [Header("Panel")]
        [SerializeField] Canvas contentPanel = null;
        #endregion

        #region Private Field
        private static List<SideUIButton> sideButtons = new List<SideUIButton>();
        #endregion

        #region UI

        #endregion
    }
}
