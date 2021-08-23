using UnityEngine;

namespace Gameboy
{
    public class GameboyController : MonoBehaviour
    {
        private void OnEnable()
        {
            GameboyAxisButton.OnAxisButtonDown += ButtonUp;
            GameboyAxisButton.OnAxisButtonDown += ButtonDown;
            GameboyAxisButton.OnAxisButtonDown += ButtonLeft;
            GameboyAxisButton.OnAxisButtonDown += ButtonRight;
            GameboyButton.OnButtonDown += ButtonA;
            GameboyButton.OnButtonDown += ButtonB;
            GameboyButton.OnButtonDown += ButtonSelect;
            GameboyButton.OnButtonDown += ButtonStart;
        }

        #region Axis Button Logics
        private void ButtonUp(GameboyAxisButtonType butonDir)
        {

        }

        private void ButtonDown(GameboyAxisButtonType butonDir)
        {

        }

        private void ButtonLeft(GameboyAxisButtonType butonDir)
        {

        }

        private void ButtonRight(GameboyAxisButtonType butonDir)
        {

        }
        #endregion

        #region Button Logics
        private void ButtonA(GameboyButtonType buttonType)
        {
            
        }

        private void ButtonB(GameboyButtonType buttonType)
        {

        }

        private void ButtonSelect(GameboyButtonType buttonType)
        {

        }

        private void ButtonStart(GameboyButtonType buttonType)
        {

        }
        #endregion

        private void OnDisable()
        {
            GameboyAxisButton.OnAxisButtonDown -= ButtonUp;
            GameboyAxisButton.OnAxisButtonDown -= ButtonDown;
            GameboyAxisButton.OnAxisButtonDown -= ButtonLeft;
            GameboyAxisButton.OnAxisButtonDown -= ButtonRight;
            GameboyButton.OnButtonDown -= ButtonA;
            GameboyButton.OnButtonDown -= ButtonB;
            GameboyButton.OnButtonDown -= ButtonSelect;
            GameboyButton.OnButtonDown -= ButtonStart;
        }
    }
}
