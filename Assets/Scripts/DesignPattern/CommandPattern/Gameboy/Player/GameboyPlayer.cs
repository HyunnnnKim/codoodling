using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameboy
{
    public class GameboyPlayer : MonoBehaviour
    {
        #region Private Field
        private Animator animator = null;
        #endregion

        private void Awake()
        {
            animator = GetComponentInChildren<Animator>();
        }

        private void OnEnable()
        {
            GameboyAxisButton.OnAxisButtonDown += ButtonDownMovement;
            GameboyAxisButton.OnAxisButtonUp += ButtonUpMovement;
        }

        #region Player Movement
        private void ButtonDownMovement(GameboyAxisButtonType butonDir)
        {
            animator.SetBool("Walk", true);
        }

        private void ButtonUpMovement(GameboyAxisButtonType butonDir)
        {
            animator.SetBool("Walk", false);
        }
        #endregion

        private void OnDisable()
        {
            GameboyAxisButton.OnAxisButtonDown -= ButtonDownMovement;
            GameboyAxisButton.OnAxisButtonUp -= ButtonUpMovement;
        }
    }
}
