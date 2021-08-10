using UnityEngine;
using UnityEngine.EventSystems;

namespace Gameboy
{
    public class GameboyAxisButton : GameboyPointerBase
    {
        #region Serialized Field
        [SerializeField] private GameboyAxisType axisType = GameboyAxisType.None;
        #endregion

        #region Button Feedbacks
        protected override void ButtonEnterFeedback(PointerEventData eventData)
        {
            if (gameObject.GetInstanceID() != eventData.selectedObject.GetInstanceID()) return;


        }
        #endregion
    }
}
