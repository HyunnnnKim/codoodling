using UnityEngine;
using UnityEngine.EventSystems;

public class UIButton : MonoBehaviour
{
    #region Serialized Field
    [Header("Button Type")]
    [SerializeField]
    private ButtonType selectedType = ButtonType.Left;
    #endregion

    #region Public Field
    public delegate void buttonDownDelegate(int sign, bool state);
    public static event buttonDownDelegate OnButtonDown = null;
    public delegate void buttonUpDelegate(bool state);
    public static event buttonUpDelegate OnButtonUp = null;
    #endregion

    #region Private Field
    private EventTrigger button = null;
    private int dir = 0;
    #endregion

    private void Start()
    {
        Init();
    }

    #region Initialize
    private void Init()
    {
        dir = selectedType.Equals(ButtonType.Left) ? -1 : +1;

        var DownEntry = new EventTrigger.Entry();
        DownEntry.eventID = EventTriggerType.PointerDown;
        DownEntry.callback.AddListener((data) => OnButtonDown?.Invoke(dir, true));

        var UpEntry = new EventTrigger.Entry();
        UpEntry.eventID = EventTriggerType.PointerUp;
        UpEntry.callback.AddListener((data) => OnButtonUp?.Invoke(false));

        button = GetComponent<EventTrigger>();
        button.triggers.Add(DownEntry);
        button.triggers.Add(UpEntry);
    }
    #endregion

    public enum ButtonType { Left, Right }
}
