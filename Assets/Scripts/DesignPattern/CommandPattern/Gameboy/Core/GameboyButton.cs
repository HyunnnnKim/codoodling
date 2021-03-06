using UnityEngine;
using UnityEngine.EventSystems;

namespace Gameboy
{
    public class GameboyButton : GameboyPointerBase
    {
        #region Serialized Field
        [Header("Button Settings")]
        [SerializeField] private GameboyButtonType buttonType = GameboyButtonType.None;
        [SerializeField] private AnimationCurvesPreset curvesPreset = null;
        #endregion

        #region Private Field
        private Collider buttonCollider = null;
        private Vector3 originPos = Vector3.zero;
        private Vector3 pressPos = Vector3.zero;
        private Coroutine buttonMovementCoroutine = null;
        #endregion

        #region Delegates
        public delegate void GameboyButtonDelegate(GameboyButtonType buttonType);
        public static event GameboyButtonDelegate OnButtonDown = null;
        public static event GameboyButtonDelegate OnButtonUp = null;
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
            // Hover in sound
            // Hover in effect
        }

        protected override void ButtonExitFeedback(PointerEventData eventData)
        {
            // Hover in sound
            // Hover in effect
        }

        protected override void ButtonDownFeedback(PointerEventData eventData)
        {
            if (buttonMovementCoroutine != null)
                StopCoroutine(buttonMovementCoroutine);
            buttonMovementCoroutine = StartCoroutine(transform.LerpLocal(pressPos, 0.6f, curvesPreset.EaseOut));

            // Hover in sound
            // Hover in effect

            OnButtonDown?.Invoke(buttonType);
        }

        protected override void ButtonUpFeedback(PointerEventData eventData)
        {
            if (buttonMovementCoroutine != null)
                StopCoroutine(buttonMovementCoroutine);
            StartCoroutine(transform.LerpLocal(originPos, 0.3f, curvesPreset.EaseOut));

            // Hover in sound
            // Hover in effect

            OnButtonUp?.Invoke(buttonType);
        }
        #endregion

        #region Button Movements
        
        #endregion

        #region Button Effects

        #endregion

        #region Button Sounds

        #endregion
    }
}
