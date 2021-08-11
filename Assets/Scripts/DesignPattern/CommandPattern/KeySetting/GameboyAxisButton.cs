using System.Collections;
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
            buttonMovementCoroutine = StartCoroutine(ButtonSmoothMovement(targetRot, curvesPreset.EaseOut, 0.6f));
            AxisButtonAction?.Invoke(axisButtonType);
        }

        protected override void ButtonUpFeedback(PointerEventData eventData)
        {
            if (buttonMovementCoroutine != null)
                StopCoroutine(buttonMovementCoroutine);
            buttonMovementCoroutine = StartCoroutine(ButtonSmoothMovement(Quaternion.identity, curvesPreset.EaseOut, 0.6f));
            AxisButtonAction?.Invoke(axisButtonType);
        }
        #endregion

        #region Button Movements
        private IEnumerator ButtonSmoothMovement(Quaternion targetRot, AnimationCurve curve, float duration)
        {
            var elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                var lerpVal = Quaternion.Lerp(axisButton.localRotation, targetRot, curve.Evaluate(elapsedTime / duration));
                axisButton.localRotation = lerpVal;
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            axisButton.localRotation = targetRot;
        }

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
