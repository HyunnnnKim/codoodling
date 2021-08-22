using UnityEngine;

namespace Gameboy
{
    public class GameboyController : MonoBehaviour
    {
        private void OnEnable()
        {
            GameboyButton.OnButtonDown += ButtonA;
        }

        #region Button Logics
        private void ButtonA(GameboyButtonType buttonType)
        {
            print("Action!");
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
            GameboyButton.OnButtonDown -= ButtonA;
        }
    }
}
