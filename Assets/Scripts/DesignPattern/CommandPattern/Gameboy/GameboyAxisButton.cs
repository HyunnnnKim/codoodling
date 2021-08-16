using UnityEngine;
using UnityEngine.EventSystems;

namespace Gameboy
{
    public class GameboyAxisButton : GameboyPointerBase
    {
        #region Serialized Field
        [Header("Axis Button Settings")]
        [SerializeField] private GameboyAxisButtonType axisButtonType = GameboyAxisButtonType.None;
        [SerializeField] private Transform axisButton = null;
        [SerializeField] private AnimationCurvesPreset curvesPreset = null;
        #endregion

        #region Delegates
        public delegate void GameboyAxisButtonDelegate(GameboyAxisButtonType axisType);
        public static event GameboyAxisButtonDelegate AxisButtonAction = null;
        #endregion

        #region Private Field
        private float pressVal = 10f;
        private Coroutine buttonMovementCoroutine = null;
        #endregion

        #region Button Feedbacks
        protected override void ButtonEnterFeedback(PointerEventData eventData)
        {

        }

        protected override void ButtonExitFeedback(PointerEventData eventData)
        {

        }

        protected override void ButtonDownFeedback(PointerEventData eventData)
        {
            if (buttonMovementCoroutine != null)
                StopCoroutine(buttonMovementCoroutine);
            var targetRot = GetTargetRotation();
            buttonMovementCoroutine = StartCoroutine(axisButton.Lerp(targetRot, 0.6f, curvesPreset.EaseOut));
            AxisButtonAction?.Invoke(axisButtonType);
        }

        protected override void ButtonUpFeedback(PointerEventData eventData)
        {
            if (buttonMovementCoroutine != null)
                StopCoroutine(buttonMovementCoroutine);
            buttonMovementCoroutine = StartCoroutine(axisButton.Lerp(Quaternion.identity, 0.6f, curvesPreset.EaseOut));
            AxisButtonAction?.Invoke(axisButtonType);
        }
        #endregion

        #region Button Movements
        private Quaternion GetTargetRotation()
        {
            switch (axisButtonType)
            {
                case GameboyAxisButtonType.Up:
                    return Quaternion.identity * Quaternion.Euler(-Vector3.forward * pressVal);
                case GameboyAxisButtonType.Down:
                    return Quaternion.identity * Quaternion.Euler(Vector3.forward * pressVal);
                case GameboyAxisButtonType.Left:
                    return Quaternion.identity * Quaternion.Euler(Vector3.up * pressVal);
                case GameboyAxisButtonType.Right:
                    return Quaternion.identity * Quaternion.Euler(-Vector3.up * pressVal);
                default:
                    return Quaternion.identity;
            }
        }
        #endregion

        #region Button Effects

        #endregion

        #region Button Sounds

        #endregion
    }
}
