using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Gameboy
{
    public class GameboyButton : GameboyPointerBase
    {
        #region Serialized Field
        [SerializeField] private GameboyButtonType buttonType = GameboyButtonType.None;
        [SerializeField] private AnimationCurvesPreset curvesPreset = null;
        #endregion

        #region Delegates
        public delegate void GameboyButtonDelegate(GameboyButtonType buttonType);
        public static event GameboyButtonDelegate ButtonAction = null;
        #endregion

        #region Private Field
        private Collider buttonCollider = null;
        private Vector3 originPos = Vector3.zero;
        private Vector3 pressPos = Vector3.zero;
        private Coroutine buttonMovementCoroutine = null;
        #endregion

        private void Start()
        {
            buttonCollider = GetComponent<Collider>();
            originPos = transform.localPosition;
            pressPos = transform.localPosition;
            pressPos.x += buttonCollider.bounds.size.x * 0.1f;
        }

        #region Initialize

        #endregion

        #region Button Feedbacks
        protected override void ButtonEnterFeedback(PointerEventData eventData)
        {
            if (gameObject.GetInstanceID() != eventData.selectedObject.GetInstanceID()) return;


        }

        protected override void ButtonExitFeedback(PointerEventData eventData)
        {
            if (gameObject.GetInstanceID() != eventData.selectedObject.GetInstanceID()) return;


        }

        protected override void ButtonDownFeedback(PointerEventData eventData)
        {
            if (gameObject.GetInstanceID() != eventData.selectedObject.GetInstanceID()) return;

            if (buttonMovementCoroutine != null)
                StopCoroutine(buttonMovementCoroutine);
            buttonMovementCoroutine = StartCoroutine(ButtonSmoothMovement(pressPos, curvesPreset.EaseOut, 0.6f));
            ButtonAction?.Invoke(buttonType);
        }

        protected override void ButtonUpFeedback(PointerEventData eventData)
        {
            if (gameObject.GetInstanceID() != eventData.selectedObject.GetInstanceID()) return;

            if (buttonMovementCoroutine != null)
                StopCoroutine(buttonMovementCoroutine);
            StartCoroutine(ButtonSmoothMovement(originPos, curvesPreset.EaseOut, 0.3f));
            ButtonAction?.Invoke(buttonType);
        }
        #endregion

        #region Button Movements
        private IEnumerator ButtonSmoothMovement(Vector3 targetPos, AnimationCurve curve, float duration)
        {
            var elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                var lerpVal = Vector3.Lerp(transform.localPosition, targetPos, curve.Evaluate(elapsedTime / duration));
                transform.localPosition = lerpVal;
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            transform.localPosition = targetPos;
        }
        #endregion

        #region Button Effects

        #endregion

        #region Button Sounds

        #endregion
    }
}
